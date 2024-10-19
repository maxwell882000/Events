using EventsBookingBackend.Domain.Common.Entities;

namespace EventsBookingBackend.Domain.Booking.Entities;

// There is will be booking rule that ensures that booking can be made no more than
// booking limits
public class BookingLimit : BaseEntity
{
    public Guid? EventId { get; set; }
    public Guid? BookingTypeId { get; set; }
    public int MaxBookings { get; set; }
    public bool IsDefault => EventId == null;
}