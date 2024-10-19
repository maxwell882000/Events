namespace EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;

public enum TransactionState
{
    Pending = 1, // Транзакция успешно создана, ожидание подтверждения (начальное состояние 0).
    Completed = 2, // Транзакция успешно завершена (начальное состояние 1).
    Canceled = -1, // Транзакция отменена (начальное состояние 1).
    CanceledAfterCompletion = -2 // Транзакция отменена после завершения (начальное состояние 2).
}