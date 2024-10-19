using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Booking.Specifications;
using EventsBookingBackend.Domain.Booking.ValueObjects;
using EventsBookingBackend.Domain.Common.Exceptions;
using Microsoft.OpenApi.Extensions;

namespace EventsBookingBackend.Domain.Booking.Services;

public class BookDomainService(
    IBookingRepository bookingRepository,
    IBookingOptionRepository bookingOptionRepository,
    IBookingLimitRepository bookingLimitRepository)
    : IBookingDomainService
{
    public async Task<Entities.Booking> CreateBooking(Entities.Booking booking)
    {
        await ValidateBookingOptionsValue(booking);
        var currentLimit = await GetBookingLimit(booking);

        var isSameBookingWithUser = await CheckSameBookingWithUser(booking);

        if (isSameBookingWithUser)
        {
            throw new DomainRuleException("Вы уже забранировали !");
        }

        var bookingCount = await SameBookingsCount(booking);

        if (currentLimit != null && bookingCount >= currentLimit.MaxBookings)
        {
            throw new DomainRuleException("Количество забранированных мест достигло лимита !");
        }

        await bookingRepository.Create(booking);

        return booking;
    }

    private async Task ValidateBookingOptionsValue(Entities.Booking booking)
    {
        var options = (await
                bookingOptionRepository.FindAll(
                    new GetBookingOptionsByIds(booking.BookingOptions.Select(e => e.OptionId).ToList())))
            .ToDictionary(e => e.Id);
        foreach (var bookingOption in booking.BookingOptions)
        {
            if (options.TryGetValue(bookingOption.OptionId, out var option))
            {
                bookingOption.Option = option;
                bookingOption.ValidateValue();
            }
        }
    }

    public async Task<bool> CheckSameBookingWithUser(Entities.Booking booking)
    {
        var allBookings = await bookingRepository
            .FindAll(new GetSimilarBookings(booking.EventId, booking.BookingTypeId));
        return allBookings.Any(e => e.UserId == booking.UserId && booking.IsSameBooking(e.BookingOptions));
    }

    public async Task<int> SameBookingsCount(Entities.Booking booking)
    {
        var allBookings = await bookingRepository
            .FindAll(new GetSimilarBookings(booking.EventId, booking.BookingTypeId));

        return allBookings.Count(e => e.IsSameBooking(booking.BookingOptions));
    }

    public async Task<BookingLimit?> GetBookingLimit(Entities.Booking booking)
    {
        return await bookingLimitRepository.FindFirst(new GetEventBookingLimit(booking.EventId))
               ?? await bookingLimitRepository.FindFirst(new GetDefaultBookingLimit(booking.BookingTypeId));
    }

    public async Task CancelBooking(Guid bookingId)
    {
        var booking = await bookingRepository.FindFirst(new GetBookingById(bookingId));
        if (booking == null)
            throw new DomainRuleException("Не существует бронирования с таким индефекатором");
        if (booking.Status != BookingStatus.Waiting)
            throw new DomainRuleException($"Бронирования уже в статусе {booking.Status.GetDisplayName()}");
        booking.Status = BookingStatus.Canceled;
        await bookingRepository.Update(booking);
    }
}