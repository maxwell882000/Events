using EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Responses;

public class CreateTransactionResponse
{
    public long CreateTime { get; set; }
    public string Transaction { get; set; }
    public TransactionState State { get; set; }
}