namespace EventsBookingBackend.Domain.Booking.ValueObjects;

public enum BookingStatus
{
    Waiting, // waiting payment
    Paid, // Ожидание команды
    Canceled, // Отменено
    PreparingToPay, // waiting payment
}