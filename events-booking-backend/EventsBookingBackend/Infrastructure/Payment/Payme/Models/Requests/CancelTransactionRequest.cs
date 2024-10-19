using EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Requests;

public class CancelTransactionRequest
{
    public string Id { get; set; }
    public TransactionReason Reason { get; set; }
}