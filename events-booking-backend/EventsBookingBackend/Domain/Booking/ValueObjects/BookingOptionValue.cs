using EventsBookingBackend.Domain.Common.ValueObjects;

namespace EventsBookingBackend.Domain.Booking.ValueObjects;

public class BookingOptionValue : BaseValueObject
{
    public string Value { get; set; }
    public int Order { get; set; }
}