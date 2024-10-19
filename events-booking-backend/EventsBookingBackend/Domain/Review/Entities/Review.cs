using EventsBookingBackend.Domain.Common.Entities;
using EventsBookingBackend.Domain.Common.ValueObjects;

namespace EventsBookingBackend.Domain.Review.Entities;

public class Review : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public int Mark { get; set; }
    public string Comment { get; set; }
    public List<FileValueObject> MediaFiles { get; set; }
}