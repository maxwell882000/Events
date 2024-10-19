using EventsBookingBackend.Domain.Common.Entities;

namespace EventsBookingBackend.Domain.Event.Entities;

public class EventAggregatedReview : BaseEntity
{
    public Event Event { get; set; }
    public Guid EventId { get; set; }
    public double OverallMark { get; set; }
    public int ReviewCount { get; set; }
}