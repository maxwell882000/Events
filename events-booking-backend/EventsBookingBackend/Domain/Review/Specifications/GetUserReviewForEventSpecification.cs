using EventsBookingBackend.Domain.Common.Specifications;

namespace EventsBookingBackend.Domain.Review.Specifications;

public class GetUserReviewForEventSpecification(Guid userId, Guid eventId) : ISpecification<Entities.Review>
{
    public IQueryable<Entities.Review> Apply(IQueryable<Entities.Review> query)
    {
        return query.Where(r => r.UserId == userId && r.EventId == eventId);
    }
}