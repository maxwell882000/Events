using System.Transactions;
using AutoMapper;
using EventsBookingBackend.Api.ControllerOptions.Types;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Booking.Specifications;
using EventsBookingBackend.Domain.Booking.ValueObjects;
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
    ITransactionRepository transactionRepository,
    IMapper mapper)
    : IPaymeService
{
    public async Task<PaymeSuccessResponse> Pay(PaymeRequest payment)
    {
        try
        {
            object? response = null;
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
        catch (PaymeMessageException e)
        {
            throw new PaymeException() { Id = payment.Id, Error = e };
        }
    }

    private async Task<GetStatementResponse> GetStatement(GetStatementRequest request)
    {
        var transaction = await transactionRepository.FindAll(new GetTransactionByTime(request.From, request.To));
        if (transaction == null)
            throw  PaymeMessageException.TransactionNotFound();
        return new GetStatementResponse()
        {
            Transactions = mapper.Map<List<GetStatementResponse.TransactionDto>>(transaction)
        };
    }

    private async Task<CheckTransactionResponse> CheckTransaction(CheckTransactionRequest request)
    {
        var transaction = await transactionRepository.FindFirst(new GetTransactionById(request.Id));
        if (transaction == null)
            throw  PaymeMessageException.TransactionNotFound();
        return mapper.Map<CheckTransactionResponse>(transaction);
    }

    private async Task<CancelTransactionResponse> CancelTransaction(CancelTransactionRequest request)
    {
        var transaction = await transactionRepository.FindFirst(new GetTransactionById(request.Id));
        if (transaction == null)
            throw  PaymeMessageException.TransactionNotFound();

        if (transaction.IsCancelledState())
            return mapper.Map<CancelTransactionResponse>(transaction);

        var booking = await bookingRepository.FindFirst(new GetBookingById(transaction.Account.BookingId));
        if (booking == null)
        {
            throw PaymeMessageException.InvalidBookingId();
        }

        if (transaction.State == TransactionState.Pending || booking.Status == BookingStatus.Canceled)
        {
            transaction.CancelTransaction(request.Reason);
            await transactionRepository.Update(transaction);
        }

        throw  PaymeMessageException.InvalidCancelOperation();
    }

    private async Task<CheckPerformTransactionResponse> CheckPerformTransaction(CheckPerformTransactionRequest request)
    {
        var booking = await bookingRepository.FindFirst(new GetBookingById(request.Account?.BookingId ?? Guid.Empty));
        if (booking == null)
            throw PaymeMessageException.InvalidBookingId();
        if (booking.Status != BookingStatus.Waiting)
            throw PaymeMessageException.InvalidBookingStatus();
        if (booking.BookingType.CostInTiyn != request.Amount)
        {
            throw PaymeMessageException.InvalidAmount();
        }

        return CheckPerformTransactionResponse.AllowRequest();
    }

    private async Task<PerformTransactionResponse> PerformTransaction(PerformTransactionRequest request)
    {
        var transaction = await transactionRepository.FindFirst(new GetTransactionById(request.Id));
        if (transaction == null)
            throw  PaymeMessageException.TransactionNotFound();
        if (transaction.IsCancelledState())
            throw  PaymeMessageException.InvalidOperation();

        if (transaction.State == TransactionState.Completed)
            return mapper.Map<PerformTransactionResponse>(transaction);
        if (transaction.CheckCreateTimeTimeout())
        {
            transaction.CancelTransactionByTimeOut();
            await transactionRepository.Update(transaction);
            throw  PaymeMessageException.InvalidOperation();
        }

        var booking = await bookingRepository.FindFirst(new GetBookingById(transaction.Account.BookingId));

        if (booking == null)
            throw PaymeMessageException.InvalidBookingId();

        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            booking.Status = BookingStatus.Paid;
            transaction.CompletedTransaction();
            await bookingRepository.Update(booking);
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
                var newTransactionDetail = mapper.Map<TransactionDetail<Account>>(request);
                newTransactionDetail.CreateTransaction();
                await transactionRepository.Create(newTransactionDetail);
                return mapper.Map<CreateTransactionResponse>(transaction);
            }
        }

        if (transaction!.State != TransactionState.Pending)
        {
            throw  PaymeMessageException.InvalidOperation();
        }

        if (transaction.CheckCreateTimeTimeout())
        {
            transaction.CancelTransactionByTimeOut();
            await transactionRepository.Update(transaction);
            throw  PaymeMessageException.InvalidOperation();
        }

        return mapper.Map<CreateTransactionResponse>(transaction);
    }
}