using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Dto;
using EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Responses;

public class GetStatementResponse
{
    public List<TransactionDto> Transactions { get; set; }

    public class TransactionDto
    {
        public string Id { get; set; } // "id"
        public long Time { get; set; } // "time"
        public decimal Amount { get; set; } // "amount"
        public AccountDto Account { get; set; } // "account"
        public long CreateTime { get; set; } // "create_time"
        public long PerformTime { get; set; } // "perform_time"
        public long CancelTime { get; set; } // "cancel_time"
        public string Transaction { get; set; } // "transaction"
        public TransactionState State { get; set; } // "state"
        public TransactionReason Reason { get; set; } // "reason"
        public List<ReceiverDto>? Receivers { get; set; }
    }
}