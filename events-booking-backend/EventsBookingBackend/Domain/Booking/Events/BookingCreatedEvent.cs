using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.ValueObjects;

namespace EventsBookingBackend.Domain.Booking.Events;

public class BookingCreatedEvent
{
    public Guid Id { get; set; }
}