using EventsBookingBackend.Domain.Common.Specifications;
using EventsBookingBackend.Domain.Event.Entities;

namespace EventsBookingBackend.Domain.Event.Specifications;

public class GetEventAggregatedReviewByEvent(Guid eventId) : ISpecification<EventAggregatedReview>
{
    public IQueryable<EventAggregatedReview> Apply(IQueryable<EventAggregatedReview> query)
    {
        return query.Where(e => e.EventId == eventId);
    }
}