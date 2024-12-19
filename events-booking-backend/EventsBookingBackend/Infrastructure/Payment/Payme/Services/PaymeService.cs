using System.Transactions;
using AutoMapper;
using EventsBookingBackend.Api.ControllerOptions.Types;
using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.Exceptions;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Booking.Services;
using EventsBookingBackend.Domain.Booking.Specifications;
using EventsBookingBackend.Domain.Booking.ValueObjects;
using EventsBookingBackend.Domain.Common.Exceptions;
using EventsBookingBackend.Infrastructure.Payment.Payme.Entities;
using EventsBookingBackend.Infrastructure.Payment.Payme.Errors;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Requests;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Responses;
using EventsBookingBackend.Infrastructure.Payment.Payme.Repositories;
using EventsBookingBackend.Infrastructure.Payment.Payme.Specifications;
using EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Services;

public class PaymeService(
    IBookingRepository bookingRepository,
    IBookingDomainService bookingDomainService,
    ITransactionRepository transactionRepository,
    IMapper mapper)
    : IPaymeService
{
    public async Task<PaymeSuccessResponse> Pay(PaymeRequest payment)
    {
        try
        {
            object? response = null;
            try
            {
                switch (payment.Method)
                {
                    case PaymeRequest.Methods.CreateTransaction:
                        response = await CreateTransaction((payment.P as CreateTransactionRequest)!);
                        break;
                    case PaymeRequest.Methods.CheckPerformTransaction:
                        response = await CheckPerformTransaction((payment.P as CheckPerformTransactionRequest)!);
                        break;
                    case PaymeRequest.Methods.PerformTransaction:
                        response = await PerformTransaction((payment.P as PerformTransactionRequest)!);
                        break;
                    case PaymeRequest.Methods.CancelTransaction:
                        response = await CancelTransaction((payment.P as CancelTransactionRequest)!);
                        break;
                    case PaymeRequest.Methods.CheckTransaction:
                        response = await CheckTransaction((payment.P as CheckTransactionRequest)!);
                        break;
                    case PaymeRequest.Methods.GetStatement:
                        response = await GetStatement((payment.P as GetStatementRequest)!);
                        break;
                }

                return new PaymeSuccessResponse()
                {
                    Id = payment.Id,
                    Result = response
                };
            }
            catch (DomainRuleException e)
            {
                switch (e.Status)
                {
                    case (int)BookingExceptionStatus.InvalidAmount:
                        throw PaymeMessageException.InvalidAmount();

                    case (int)BookingExceptionStatus.InvalidPaymentStatus:
                        throw PaymeMessageException.InvalidBookingStatus();
                    default:
                        throw PaymeMessageException.InvalidBookingId();
                }
            }
        }
        catch (PaymeMessageException e)
        {
            throw new PaymeException() { Id = payment.Id, Error = e };
        }
    }

    private async Task<GetStatementResponse> GetStatement(GetStatementRequest request)
    {
        List<TransactionDetail<Account>> transaction =
            await transactionRepository.FindAll(new GetTransactionByTime(request.From, request.To));
        if (transaction == null)
            throw PaymeMessageException.TransactionNotFound();
        return new GetStatementResponse()
        {
            Transactions = mapper.Map<List<GetStatementResponse.TransactionDto>>(transaction)
        };
    }

    private async Task<CheckTransactionResponse> CheckTransaction(CheckTransactionRequest request)
    {
        var transaction = await transactionRepository.FindFirst(new GetTransactionById(request.Id));
        if (transaction == null)
            throw PaymeMessageException.TransactionNotFound();
        return mapper.Map<CheckTransactionResponse>(transaction);
    }

    private async Task<CancelTransactionResponse> CancelTransaction(CancelTransactionRequest request)
    {
        var transaction = await transactionRepository.FindFirst(new GetTransactionById(request.Id));
        if (transaction == null)
            throw PaymeMessageException.TransactionNotFound();

        if (transaction.IsCancelledState())
            return mapper.Map<CancelTransactionResponse>(transaction);

        Booking? booking = await bookingRepository.FindFirst(new GetBookingById(transaction.Account.BookingId));
        if (booking == null)
        {
            throw PaymeMessageException.InvalidBookingId();
        }

        if (transaction.State == TransactionState.Pending || booking.Status == BookingStatus.Canceled)
        {
            transaction.CancelTransaction(request.Reason);
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (booking.Status != BookingStatus.Canceled)
                {
                    await bookingDomainService.CancelBooking(booking.Id);
                }

                await transactionRepository.Update(transaction);
                transactionScope.Complete();
            }

            return mapper.Map<CancelTransactionResponse>(transaction);
        }

        throw PaymeMessageException.InvalidCancelOperation();
    }

    private async Task<CheckPerformTransactionResponse> CheckPerformTransaction(CheckPerformTransactionRequest request)
    {
        await bookingDomainService.CheckBookingPayable(request.Account?.BookingId ?? Guid.Empty,
            request.Amount ?? 0);
        return CheckPerformTransactionResponse.AllowRequest();
    }

    private async Task<PerformTransactionResponse> PerformTransaction(PerformTransactionRequest request)
    {
        var transaction = await transactionRepository.FindFirst(new GetTransactionById(request.Id));
        if (transaction == null)
            throw PaymeMessageException.TransactionNotFound();
        if (transaction.IsCancelledState())
            throw PaymeMessageException.InvalidOperation();

        if (transaction.State == TransactionState.Completed)
            return mapper.Map<PerformTransactionResponse>(transaction);
        if (transaction.CheckCreateTimeTimeout())
        {
            transaction.CancelTransactionByTimeOut();
            await transactionRepository.Update(transaction);
            throw PaymeMessageException.InvalidOperation();
        }

        var booking = await bookingRepository.FindFirst(new GetBookingById(transaction.Account.BookingId));

        if (booking == null)
            throw PaymeMessageException.InvalidBookingId();

        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await bookingDomainService.PaidBooking(transaction.Account.BookingId, transaction.Amount);
            transaction.CompletedTransaction();
            await transactionRepository.Update(transaction);
            transactionScope.Complete();
            return mapper.Map<PerformTransactionResponse>(transaction);
        }
    }

    private async Task<CreateTransactionResponse> CreateTransaction(CreateTransactionRequest request)
    {
        var transaction = await transactionRepository.FindFirst(new GetTransactionById(request.Id));
        if (transaction is null)
        {
            var response = await CheckPerformTransaction(mapper.Map<CheckPerformTransactionRequest>(request));
            if (response.Allow)
            {
                Booking? booking =
                    await bookingRepository.FindFirst(new GetBookingById(request.Account!.BookingId));
                if (booking!.Status == BookingStatus.PreparingToPay)
                {
                    throw PaymeMessageException.TransactionCreated();
                }

                var newTransactionDetail = mapper.Map<TransactionDetail<Account>>(request);
                newTransactionDetail.CreateTransaction();
                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await transactionRepository.Create(newTransactionDetail);
                    booking.Status = BookingStatus.PreparingToPay;
                    await bookingRepository.Update(booking);
                    transactionScope.Complete();
                }

                return mapper.Map<CreateTransactionResponse>(newTransactionDetail);
            }
        }

        if (transaction!.State != TransactionState.Pending)
        {
            throw PaymeMessageException.InvalidOperation();
        }

        if (transaction.CheckCreateTimeTimeout())
        {
            transaction.CancelTransactionByTimeOut();
            await transactionRepository.Update(transaction);
            throw PaymeMessageException.InvalidOperation();
        }

        return mapper.Map<CreateTransactionResponse>(transaction);
    }
}