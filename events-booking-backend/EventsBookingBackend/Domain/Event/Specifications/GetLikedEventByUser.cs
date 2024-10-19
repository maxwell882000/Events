using EventsBookingBackend.Domain.Common.Specifications;
using EventsBookingBackend.Domain.Event.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Domain.Event.Specifications;

public class GetLikedEventByUser(Guid userId) : ISpecification<LikedEvent>
{
    public IQueryable<LikedEvent> Apply(IQueryable<LikedEvent> query)
    {
        return query
            .Include(e => e.Event)
            .ThenInclude(e => e.AggregatedReviews)
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.CreatedAt);
    }
}