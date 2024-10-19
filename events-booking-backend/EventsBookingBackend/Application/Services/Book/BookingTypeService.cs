using AutoMapper;
using EventsBookingBackend.Application.Models.Booking.Requests;
using EventsBookingBackend.Application.Models.Booking.Responses;
using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Booking.Specifications;

namespace EventsBookingBackend.Application.Services.Book;

public class BookingTypeService(IMapper mapper, IBookingTypeRepository bookingTypeRepository) : IBookingTypeService
{
    public async Task<List<GetBookingTypeByCategoryResponse>> GetBookingType(GetBookingTypeByCategoryRequest request)
    {
        var bookingType = await
            bookingTypeRepository.FindAll(new GetBookingTypeByCategory(request.CategoryId));
        return mapper.Map<List<GetBookingTypeByCategoryResponse>>(bookingType);
    }


    public async Task<CreateBookingTypeResponse> CreateBookingType(CreateBookingTypeRequest request)
    {
        var bookingType = mapper.Map<BookingType>(request);
        await bookingTypeRepository.Create(bookingType);
        return new CreateBookingTypeResponse()
        {
            Id = bookingType.Id,
        };
    }
}