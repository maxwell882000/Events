using EventsBookingBackend.Domain.Common.Entities;
using EventsBookingBackend.Domain.Common.ValueObjects;
using EventsBookingBackend.Domain.Event.ValueObjects;

namespace EventsBookingBackend.Domain.Event.Entities;

public class Event : BaseEntity
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public EventState State { get; set; }
    public FileValueObject PreviewImage { get; set; }
    public List<FileValueObject> Images { get; set; } = new();
    public Building Building { get; set; }
    public IList<LikedEvent>? LikedEvents { get; set; } = new List<LikedEvent>();
    public EventAggregatedReview? AggregatedReviews { get; set; }
}