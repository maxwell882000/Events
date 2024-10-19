using EventsBookingBackend.Application.Models.Booking.Requests;
using EventsBookingBackend.Application.Models.Booking.Responses;

namespace EventsBookingBackend.Application.Services.Book;

public interface IBookService
{
    public Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request);

    public Task CancelBooking(CancelBookingRequest request);

    public Task<GetSameBookingsCountResponse> GetSameBookingsCount(GetSameBookingsCountRequest request);
}