using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.ValueObjects;
using EventsBookingBackend.Domain.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Domain.Booking.Specifications;

public class CheckUserInBookingGroup(Guid eventId, Guid userId, Guid bookingTypeId, string options)
    : ISpecification<BookingGroup>
{
    public IQueryable<BookingGroup> Apply(IQueryable<BookingGroup> query)
    {
        return query.Include(e => e.Bookings)
            .Where(e => e.Bookings.Any(b => b.UserId == userId)
                        && e.EventId == eventId
                        && e.BookingTypeId == bookingTypeId
                        && e.UserOptions == options);
    }
}