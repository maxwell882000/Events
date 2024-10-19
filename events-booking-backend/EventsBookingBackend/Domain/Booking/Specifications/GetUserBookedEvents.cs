using EventsBookingBackend.Domain.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Domain.Booking.Specifications;

public class GetUserBookedEvents(Guid userId) : ISpecification<Entities.Booking>
{
    public IQueryable<Entities.Booking> Apply(IQueryable<Entities.Booking> query)
    {
        return query
            .Include(e => e.Event)
            .Include(e => e.BookingType)
            .Include(e => e.BookingOptions)
            .ThenInclude(e => e.BookingOptionValue)
            .Include(e=> e.BookingOptions)
            .ThenInclude(e=> e.Option)
            .Where(b => b.UserId == userId)
            .OrderByDescending(e => e.CreatedAt)
            .AsNoTracking();
    }
}