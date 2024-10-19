using EventsBookingBackend.Application.Models.Booking.Requests;
using EventsBookingBackend.Application.Models.Booking.Responses;

namespace EventsBookingBackend.Application.Services.Book;

public interface IBookingTypeService
{
    public Task<List<GetBookingTypeByCategoryResponse>> GetBookingType(GetBookingTypeByCategoryRequest request);
    public Task<CreateBookingTypeResponse> CreateBookingType(CreateBookingTypeRequest request);
}