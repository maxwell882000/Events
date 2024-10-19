using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Common.Specifications;

namespace EventsBookingBackend.Domain.Booking.Specifications;

public class GetBookingEventById(Guid id) : ISpecification<BookingEvent>
{
    public IQueryable<BookingEvent> Apply(IQueryable<BookingEvent> query)
    {
        return query.Where(e => e.Id == id);
    }
}