using EventsBookingBackend.Domain.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Domain.Booking.Specifications;

public class GetBookingById(Guid bookingId) : ISpecification<Entities.Booking>
{
    public IQueryable<Entities.Booking> Apply(IQueryable<Entities.Booking> query)
    {
        return query
            .Include(e => e.BookingGroup)
            .ThenInclude(e => e.BookingType)
            .Include(e => e.BookingType)
            .Include(e => e.BookingOptions.OrderByDescending(e => e.OptionId))
            .Where(e => e.Id == bookingId);
    }
}