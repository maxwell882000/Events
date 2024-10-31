using EventsBookingBackend.Domain.Booking.Entities;

namespace EventsBookingBackend.Domain.Booking.Services;

public interface IBookingTypeDomainService
{
    Task<List<BookingType>> GetBookingTypesWithOptions(Guid categoryId, Guid? eventId);
    Task<List<BookingType>> GetBookingTypes(Guid categoryId, Guid? eventId);
}