using EventsBookingBackend.Domain.Booking.Entities;

namespace EventsBookingBackend.Domain.Booking.Services;

public interface IBookingDomainService
{
    public Task<Entities.Booking> CreateBooking(Entities.Booking booking);
    public Task<int> SameBookingsCount(Entities.Booking booking);
    public Task<BookingLimit?> GetBookingLimit(Entities.Booking booking);
    public Task CancelBooking(Guid bookingId);
}