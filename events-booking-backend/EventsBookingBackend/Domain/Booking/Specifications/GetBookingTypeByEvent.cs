using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Domain.Booking.Specifications;

public class GetBookingTypeByEvent(Guid eventId) : ISpecification<BookingType>
{
    public IQueryable<BookingType> Apply(IQueryable<BookingType> query)
    {
        return query
            .Include(e => e.BookingOptions.OrderBy(op => op.Order))
            .Where(bookingType => bookingType.EventId == eventId).AsNoTracking();
    }
}