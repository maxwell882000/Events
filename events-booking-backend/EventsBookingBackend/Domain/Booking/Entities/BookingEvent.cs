using EventsBookingBackend.Domain.Common.Entities;

namespace EventsBookingBackend.Domain.Booking.Entities;

public class BookingEvent : BaseEntity
{
    public string Name { get; set; }
}