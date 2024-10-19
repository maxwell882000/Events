using AutoMapper;
using EventsBookingBackend.Application.Common.Exceptions;
using EventsBookingBackend.Application.Models.Booking.Requests;
using EventsBookingBackend.Application.Models.Booking.Responses;
using EventsBookingBackend.Application.Services.Auth;
using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Booking.Services;
using EventsBookingBackend.Domain.Booking.Specifications;
using EventsBookingBackend.Domain.Event.Repositories;
using EventsBookingBackend.Domain.Event.Specifications;

namespace EventsBookingBackend.Application.Services.Book;

public class BookService(
    IBookingDomainService bookingDomainService,
    IEventRepository eventRepository,
    IAuthService authService,
    IMapper mapper) : IBookService
{
    public async Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request)
    {
        var booking = mapper.Map<Booking>(request);
        booking.UserId = (Guid)authService.GetCurrentAuthUserId()!;
        var eventEntity = await eventRepository.FindFirst(new GetEventByIdSpecification((Guid)request.EventId!));
        if (eventEntity == null)
            throw new AppValidationException("Не найдено событие !");
        await bookingDomainService.CreateBooking(booking);
        return new CreateBookingResponse()
        {
            BookingId = booking.Id,
        };
    }

    public async Task CancelBooking(CancelBookingRequest request)
    {
        await bookingDomainService.CancelBooking(request.BookingId);
    }

    public async Task<GetSameBookingsCountResponse> GetSameBookingsCount(GetSameBookingsCountRequest request)
    {
        var booking = mapper.Map<Booking>(request);
        var bookingCount = await bookingDomainService.SameBookingsCount(booking);
        var bookingLimit = await bookingDomainService.GetBookingLimit(booking);
        return new GetSameBookingsCountResponse()
        {
            Count = bookingCount,
            TotalCount = bookingLimit?.MaxBookings ?? 0
        };
    }
}