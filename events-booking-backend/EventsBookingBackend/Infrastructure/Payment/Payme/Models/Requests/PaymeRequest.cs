using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Dto;
using EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;
using Newtonsoft.Json.Linq;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Requests;

public class PaymeRequest
{
    public int? Id { get; set; }
    public JObject Params { get; set; }
    public Methods? Method { get; set; }

    public enum Methods
    {
        PerformTransaction,
        CreateTransaction,
        CheckPerformTransaction,
        CancelTransaction,
        CheckTransaction,
        GetStatement,
    }

    public object P => Method switch
    {
        Methods.PerformTransaction => new PerformTransactionRequest()
        {
            Id = Params["id"]?.Value<string>() ?? "",
        },
        Methods.CreateTransaction => new CreateTransactionRequest()
        {
            Id = Params["id"]?.Value<string>() ?? "",
            Time = Params["time"]?.Value<long>() ?? 0,
            Amount = Params["amount"]?.Value<long>() ?? 0,
            Account = new AccountDto()
            {
                BookingId = Params["account"]?["bookingId"]?.Value<Guid>() ?? Guid.Empty // Extract BookingId
            },
        },
        Methods.CheckPerformTransaction => new CheckPerformTransactionRequest()
        {
            Amount = Params["amount"]?.Value<long>() ?? 0,
            Account = new AccountDto()
            {
                BookingId = Params["account"]?["bookingId"]?.Value<Guid>() ?? Guid.Empty // Extract BookingId
            }
        },
        Methods.CancelTransaction => new CancelTransactionRequest()
        {
            Id = Params["id"]?.Value<string>() ?? "",
            Reason = Params["reason"]?.Value<TransactionReason>() ?? TransactionReason.UnknownError,
        },
        Methods.CheckTransaction => new CheckTransactionRequest()
        {
            Id = Params["id"]?.Value<string>() ?? "",
        },
        Methods.GetStatement => new GetStatementRequest()
        {
            From = Params["from"]?.Value<long>() ?? 0,
            To = Params["to"]?.Value<long>() ?? 0,
        },
        _ => throw new InvalidOperationException("Unsupported method")
    };
}