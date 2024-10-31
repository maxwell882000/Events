using System.Text;
using AutoMapper;
using EventsBookingBackend.Application.Common.Exceptions;
using EventsBookingBackend.Application.Models.Booking.Requests;
using EventsBookingBackend.Application.Models.Booking.Responses;
using EventsBookingBackend.Application.Services.Auth;
using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.Services;
using EventsBookingBackend.Domain.Event.Repositories;
using EventsBookingBackend.Domain.Event.Specifications;
using EventsBookingBackend.Infrastructure.Payment.Payme.Option;
using Microsoft.Extensions.Options;

namespace EventsBookingBackend.Application.Services.Book;

public class BookService(
    IBookingDomainService bookingDomainService,
    IEventRepository eventRepository,
    IAuthService authService,
    IOptions<PaymeOption> paymeOption,
    IMapper mapper) : IBookService
{
    public async Task<CreateBookingResponse> CreateBooking(CreateBookingRequest request)
    {
        var booking = mapper.Map<Booking>(request);
        booking.UserId = (Guid)authService.GetCurrentAuthUserId()!;
        var eventEntity = await eventRepository.FindFirst(new GetEventByIdSpecification((Guid)request.EventId!));
        if (eventEntity == null)
            throw new AppValidationException("Не найдено событие !");
        booking = await bookingDomainService.CreateBooking(booking);
        return new CreateBookingResponse()
        {
            BookingId = booking.Id,
            PaymentUrl = GeneratePaymentUrl(request, booking)
        };
    }

    private string GeneratePaymentUrl(CreateBookingRequest request, Booking booking)
    {
        return request.Payment switch
        {
            CreateBookingRequest.PaymentOption.PAYME => GeneratePaymeUrl(request, booking),
            CreateBookingRequest.PaymentOption.CLICK => GenerateClickUrl(request),
            _ => throw new NotSupportedException("Payment option not supported.")
        };
    }

    private string GenerateClickUrl(CreateBookingRequest request)
    {
        return "";
    }

    private string GeneratePaymeUrl(CreateBookingRequest request, Booking booking)
    {
        var merchantId = paymeOption.Value.MerchantId;
        string parameters = $"m={merchantId};ac.booking_id={booking.Id};a={booking.BookingType.CostInTiyn}";
        string base64EncodedParameters = Convert.ToBase64String(Encoding.UTF8.GetBytes(parameters));

        return $"https://checkout.paycom.uz/{base64EncodedParameters}";
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