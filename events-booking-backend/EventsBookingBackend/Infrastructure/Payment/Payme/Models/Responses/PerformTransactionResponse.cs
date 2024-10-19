using EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Responses;

public class PerformTransactionResponse
{
    public string Transaction { get; set; } // "transaction"
    public long PerformTime { get; set; } // "perform_time"
    public TransactionState State { get; set; } // "state"
}