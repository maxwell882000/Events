namespace EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;

public enum TransactionReason
{
    RecipientsNotFoundOrInactive = 1, // Один или несколько получателей не найдены или неактивны в Payme Business.
    DebitOperationError = 2, // Ошибка при выполнении дебетовой операции в процессинговом центре.
    TransactionExecutionError = 3, // Ошибка выполнения транзакции.
    TransactionCanceledDueToTimeout = 4, // Транзакция отменена по таймауту.
    Refund = 5, // Возврат денег.
    UnknownError = 10 // Неизвестная ошибка.
}