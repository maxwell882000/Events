using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Domain.Booking.Specifications;

public class GetBookingGroup(Guid eventId, Guid bookingTypeId, string options) : ISpecification<BookingGroup>
{
    public IQueryable<BookingGroup> Apply(IQueryable<BookingGroup> query)
    {
        return query
            .Include(e=> e.Bookings)
            .Where(e => e.BookingTypeId == bookingTypeId
                        && e.EventId == eventId
                        && e.UserOptions == options);
    }
}