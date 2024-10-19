using EventsBookingBackend.Domain.Common.Specifications;

namespace EventsBookingBackend.Domain.Review.Specifications;

public class GetEventReviews(Guid eventId): ISpecification<Entities.Review>
{
    public IQueryable<Entities.Review> Apply(IQueryable<Entities.Review> query)
    {
        return query.Where(e => e.EventId == eventId);
    }
}