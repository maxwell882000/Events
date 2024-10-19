using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Common.Specifications;

namespace EventsBookingBackend.Domain.Booking.Specifications;

public class GetDefaultBookingLimit(Guid bookingTypeId) : ISpecification<BookingLimit>
{
    public IQueryable<BookingLimit> Apply(IQueryable<BookingLimit> query)
    {
        return query.Where(l => l.BookingTypeId == bookingTypeId && l.EventId == null);
    }
}