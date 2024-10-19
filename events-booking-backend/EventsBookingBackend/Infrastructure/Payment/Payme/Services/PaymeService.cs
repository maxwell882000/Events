using System.Transactions;
using AutoMapper;
using EventsBookingBackend.Api.ControllerOptions.Types;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Booking.Specifications;
using EventsBookingBackend.Domain.Booking.ValueObjects;
using EventsBookingBackend.Infrastructure.Payment.Payme.Entities;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Errors;
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
                    response = await CreateTransaction((payment.Params as CreateTransactionRequest)!);
                    break;
                case PaymeRequest.Methods.CheckPerformTransaction:
                    response = await CheckPerformTransaction((payment.Params as CheckPerformTransactionRequest)!);
                    break;
                case PaymeRequest.Methods.PerformTransaction:
                    response = await PerformTransaction((payment.Params as PerformTransactionRequest)!);
                    break;
                case PaymeRequest.Methods.CancelTransaction:
                    response = await CancelTransaction((payment.Params as CancelTransactionRequest)!);
                    break;
                case PaymeRequest.Methods.CheckTransaction:
                    response = await CheckTransaction((payment.Params as CheckTransactionRequest)!);
                    break;
                case PaymeRequest.Methods.GetStatement:
                    response = await GetStatement((payment.Params as GetStatementRequest)!);
                    break;
            }

            return new PaymeSuccessResponse()
            {
                Id = payment.Id,
                Result = response
            };
        }
        catch (PaymeErrorResponse e)
        {
            throw new PaymeErrorModel() { Id = payment.Id, Error = e };
        }
    }

    private async Task<GetStatementResponse> GetStatement(GetStatementRequest request)
    {
        var transaction = await transactionRepository.FindAll(new GetTransactionByTime(request.From, request.To));
        if (transaction == null)
            throw new PaymeErrorResponse() { Code = PaymeErrors.TransactionNotFound };
        return new GetStatementResponse()
        {
            Transactions = mapper.Map<List<GetStatementResponse.TransactionDto>>(transaction)
        };
    }

    private async Task<CheckTransactionResponse> CheckTransaction(CheckTransactionRequest request)
    {
        var transaction = await transactionRepository.FindFirst(new GetTransactionById(request.Id));
        if (transaction == null)
            throw new PaymeErrorResponse() { Code = PaymeErrors.TransactionNotFound };
        return mapper.Map<CheckTransactionResponse>(transaction);
    }

    private async Task<CancelTransactionResponse> CancelTransaction(CancelTransactionRequest request)
    {
        var transaction = await transactionRepository.FindFirst(new GetTransactionById(request.Id));
        if (transaction == null)
            throw new PaymeErrorResponse() { Code = PaymeErrors.TransactionNotFound };

        if (transaction.IsCancelledState())
            return mapper.Map<CancelTransactionResponse>(transaction);

        var booking = await bookingRepository.FindFirst(new GetBookingById(transaction.Account.BookingId));
        if (booking == null)
        {
            throw PaymeErrorResponse.InvalidBookingId();
        }

        if (transaction.State == TransactionState.Pending || booking.Status == BookingStatus.Canceled)
        {
            transaction.CancelTransaction(request.Reason);
            await transactionRepository.Update(transaction);
        }

        throw new PaymeErrorResponse() { Code = PaymeErrors.InvalidCancelOperation };
    }

    private async Task<CheckPerformTransactionResponse> CheckPerformTransaction(CheckPerformTransactionRequest request)
    {
        var booking = await bookingRepository.FindFirst(new GetBookingById(request.Account?.BookingId ?? Guid.Empty));
        if (booking == null)
            throw PaymeErrorResponse.InvalidBookingId();
        if (booking.Status != BookingStatus.Waiting)
            throw PaymeErrorResponse.InvalidBookingStatus();
        if (booking.BookingType.CostInTiyn != request.Amount)
        {
            throw new PaymeErrorResponse() { Code = PaymeErrors.InvalidAmount };
        }

        return CheckPerformTransactionResponse.AllowRequest();
    }

    private async Task<PerformTransactionResponse> PerformTransaction(PerformTransactionRequest request)
    {
        var transaction = await transactionRepository.FindFirst(new GetTransactionById(request.Id));
        if (transaction == null)
            throw new PaymeErrorResponse() { Code = PaymeErrors.TransactionNotFound };
        if (transaction.IsCancelledState())
            throw new PaymeErrorResponse() { Code = PaymeErrors.InvalidOperation };

        if (transaction.State == TransactionState.Completed)
            return mapper.Map<PerformTransactionResponse>(transaction);
        if (transaction.CheckCreateTimeTimeout())
        {
            transaction.CancelTransactionByTimeOut();
            await transactionRepository.Update(transaction);
            throw new PaymeErrorResponse() { Code = PaymeErrors.InvalidOperation };
        }

        var booking = await bookingRepository.FindFirst(new GetBookingById(transaction.Account.BookingId));

        if (booking == null)
            throw PaymeErrorResponse.InvalidBookingId();

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
            throw new PaymeErrorResponse() { Code = PaymeErrors.InvalidOperation };
        }

        if (transaction.CheckCreateTimeTimeout())
        {
            transaction.CancelTransactionByTimeOut();
            await transactionRepository.Update(transaction);
            throw new PaymeErrorResponse() { Code = PaymeErrors.InvalidOperation };
        }

        return mapper.Map<CreateTransactionResponse>(transaction);
    }
}