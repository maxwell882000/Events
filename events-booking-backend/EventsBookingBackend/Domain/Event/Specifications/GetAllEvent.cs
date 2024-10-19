using EventsBookingBackend.Domain.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Domain.Event.Specifications;

public class GetAllEventsSpecification(Guid? userId) : ISpecification<Entities.Event>
{
    public IQueryable<Entities.Event> Apply(IQueryable<Entities.Event> query)
    {
        return query
            .Include(e => e.LikedEvents.Where(l => l.UserId == userId))
            .Include(e => e.AggregatedReviews);
    }
}