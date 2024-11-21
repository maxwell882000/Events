using EventsBookingBackend.Domain.Booking.Entities;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Services;

public interface IPaymeGenerateUrlService
{
    string GenerateUrl(string bookingId, long amount);
}