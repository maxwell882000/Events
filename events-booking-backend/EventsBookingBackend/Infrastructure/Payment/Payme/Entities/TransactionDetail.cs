using EventsBookingBackend.Domain.Common.Entities;
using EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Entities;

public class TransactionDetail<T> : BaseEntity
{
    private int TIMEOUT_IN_MILLISECONDS = 1000 * 60 * 10;
    public string PaymeId { get; set; } // "id"
    public long Time { get; set; } // "time"
    public decimal Amount { get; set; } // "amount"
    public T Account { get; set; } // "account"
    public long CreateTime { get; set; } // "create_time"
    public long PerformTime { get; set; } = 0; // "perform_time"
    public long CancelTime { get; set; } = 0; // "cancel_time"
    public TransactionState State { get; set; } // "state"
    public TransactionReason Reason { get; set; } // "reason"
    public List<Receiver> Receivers { get; set; } // "receivers"

    public void CreateTransaction()
    {
        State = TransactionState.Pending;
        CreateTime = ConvertDateTimeToUnixTimestamp(DateTime.Now);
    }

    public void CompletedTransaction()
    {
        State = TransactionState.Completed;
        PerformTime = ConvertDateTimeToUnixTimestamp(DateTime.Now);
    }

    public bool IsCancelledState()
    {
        return State != TransactionState.Completed && State != TransactionState.Completed;
    }

    public void CancelTransaction(TransactionReason reason)
    {
        State = TransactionState.Canceled;
        Reason = reason;
        CancelTime = ConvertDateTimeToUnixTimestamp(DateTime.Now);
    }

    public bool CheckCreateTimeTimeout()
    {
        return CreateTime - ConvertDateTimeToUnixTimestamp(DateTime.Now) > TIMEOUT_IN_MILLISECONDS;
    }

    public void CancelTransactionByTimeOut()
    {
        State = TransactionState.Canceled;
        Reason = TransactionReason.TransactionCanceledDueToTimeout;
    }

    private long ConvertDateTimeToUnixTimestamp(DateTime dateTime)
    {
        DateTime utcDateTime = dateTime.ToUniversalTime();

        long unixTimestamp = (long)(utcDateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;

        return unixTimestamp;
    }
}