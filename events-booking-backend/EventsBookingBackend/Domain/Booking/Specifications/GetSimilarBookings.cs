using EventsBookingBackend.Domain.Booking.ValueObjects;
using EventsBookingBackend.Domain.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Domain.Booking.Specifications;

public class GetSimilarBookings(Guid eventId, Guid bookingTypeId) : ISpecification<Entities.Booking>
{
    public IQueryable<Entities.Booking> Apply(IQueryable<Entities.Booking> query)
    {
        return query
            .Include(e=> e.BookingOptions)
            .Where(e =>
            e.EventId == eventId
            && e.BookingTypeId == bookingTypeId
            && e.Status != BookingStatus.Canceled);
    }
}