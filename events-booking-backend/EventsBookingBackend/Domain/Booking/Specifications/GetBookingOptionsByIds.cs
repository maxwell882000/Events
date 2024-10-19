using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Common.Specifications;

namespace EventsBookingBackend.Domain.Booking.Specifications;

public class GetBookingOptionsByIds(IList<Guid> ids) : ISpecification<BookingOption>
{
    public IQueryable<BookingOption> Apply(IQueryable<BookingOption> query)
    {
        return query.Where(b => ids.Contains(b.Id));
    }
}