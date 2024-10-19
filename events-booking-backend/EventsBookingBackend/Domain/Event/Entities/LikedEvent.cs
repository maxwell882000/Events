using EventsBookingBackend.Domain.Common.Entities;

namespace EventsBookingBackend.Domain.Event.Entities;

public class LikedEvent : BaseEntity
{
    public Guid EventId { get; set; }
    public Guid UserId { get; set; }

    public Event Event { get; set; }
}