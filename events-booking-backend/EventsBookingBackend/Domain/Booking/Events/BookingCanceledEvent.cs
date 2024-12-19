using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.ValueObjects;

namespace EventsBookingBackend.Domain.Booking.Events;

public class BookingCanceledEvent
{
    public BookingStatus Status { get; set; } = BookingStatus.Waiting;
    public Guid EventId { get; set; }
    public Guid UserId { get; set; }
    public Guid BookingTypeId { get; set; }
    public Guid? BookingGroupId { get; set; }
    public List<BookingUserOption> BookingOptions { get; set; }
    public BookingType BookingType { get; set; }
    public BookingEvent Event { get; set; }
    public BookingGroup? BookingGroup { get; set; }
}