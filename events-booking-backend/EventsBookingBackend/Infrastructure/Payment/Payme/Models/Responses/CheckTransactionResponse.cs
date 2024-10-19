using EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Responses;

public class CheckTransactionResponse
{
    public long CreateTime { get; set; } // "create_time"
    public long PerformTime { get; set; } // "perform_time"
    public long CancelTime { get; set; } // "cancel_time"
    public string Transaction { get; set; } // "transaction"
    public TransactionState State { get; set; } // "state"
    public TransactionReason Reason { get; set; } // "reason"
}