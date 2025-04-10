using EventsBookingBackend.Application.Models.Booking.Responses;
using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Booking.Specifications;

namespace EventsBookingBackend.Domain.Booking.Services;

public class BookingTypeDomainService(IBookingTypeRepository bookingTypeRepository) : IBookingTypeDomainService
{
    public async Task<List<BookingType>> GetBookingTypesWithOptions(Guid categoryId, Guid? eventId)
    {
        if (eventId.HasValue)
        {
            var bookingTypeByEvent = await
                bookingTypeRepository.FindAll(new GetBookingTypeByEvent(eventId.Value));

            if (bookingTypeByEvent.Any())
                return bookingTypeByEvent;
        }

        return await
            bookingTypeRepository.FindAll(new GetBookingTypeByCategory(categoryId));
    }

    public Task<List<BookingType>> GetBookingTypes(Guid categoryId, Guid? eventId)
    {
        throw new NotImplementedException();
    }
}