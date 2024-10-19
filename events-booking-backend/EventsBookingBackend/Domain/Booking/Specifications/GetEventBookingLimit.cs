using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Common.Specifications;

namespace EventsBookingBackend.Domain.Booking.Specifications;

public class GetEventBookingLimit(Guid eventId) : ISpecification<BookingLimit>
{
    public IQueryable<BookingLimit> Apply(IQueryable<BookingLimit> query)
    {
        return query.Where(booking => booking.EventId == eventId);
    }
}