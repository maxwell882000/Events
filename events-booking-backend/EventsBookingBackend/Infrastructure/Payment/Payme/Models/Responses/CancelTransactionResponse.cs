using EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Responses;

public class CancelTransactionResponse
{
    public string Transaction { get; set; } // "transaction"
    public long CancelTime { get; set; } // "cancel_time"
    public TransactionState State { get; set; } // "state"
}