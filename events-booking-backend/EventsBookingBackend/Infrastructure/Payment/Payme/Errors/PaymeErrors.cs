namespace EventsBookingBackend.Infrastructure.Payment.Payme.Errors;

public enum PaymeErrors
{
    InvalidAmount = -31001,
    TransactionNotFound = -31003,
    InvalidOperation = -31008,
    InvalidCancelOperation = -31007,
    InvalidBookingId = -31050,
    InvalidBookingStatus = -31051,
    TransactionCreated = -31052,
    InvalidAuthorization = -32504,
}