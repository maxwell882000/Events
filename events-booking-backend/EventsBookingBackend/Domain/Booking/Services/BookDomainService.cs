using System.Transactions;
using AutoMapper;
using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.Events;
using EventsBookingBackend.Domain.Booking.Exceptions;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Booking.Specifications;
using EventsBookingBackend.Domain.Booking.ValueObjects;
using EventsBookingBackend.Domain.Common.Exceptions;
using MassTransit;
using Microsoft.OpenApi.Extensions;

namespace EventsBookingBackend.Domain.Booking.Services;

public class BookDomainService(
    IBookingRepository bookingRepository,
    IBookingGroupRepository bookingGroupRepository,
    IBookingOptionRepository bookingOptionRepository,
    IBookingLimitRepository bookingLimitRepository,
    IPublishEndpoint publishEndpoint,
    IMapper mapper
)
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

        var bookingCount = await GetBookingGroup(booking);

        if (bookingCount?.IsMaxLimitReached(currentLimit) == true)
        {
            throw new DomainRuleException("Количество забранированных мест достигло лимита !");
        }

        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await bookingRepository.Create(booking);
            BookingGroup? group = await GetBookingGroup(booking);
            if (group == null)
            {
                group = BookingGroup.FromBooking(booking);
                await bookingGroupRepository.Create(group);
            }

            group.AddBooking(booking, currentLimit);
            await bookingGroupRepository.Update(group);
            await publishEndpoint.Publish(mapper.Map<BookingCreatedEvent>(booking));
            transactionScope.Complete();
        }


        return (await bookingRepository.FindFirst(new GetBookingById(booking.Id)))!;
    }

    private async Task<BookingGroup?> GetBookingGroup(Entities.Booking booking)
    {
        var group = await bookingGroupRepository.FindFirst(new GetBookingGroup(booking.EventId, booking.BookingTypeId,
            booking.HashOptions()));
        return group;
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
        var group = await bookingGroupRepository.FindFirst(new CheckUserInBookingGroup(
            booking.EventId,
            booking.UserId,
            booking.BookingTypeId,
            booking.HashOptions()));
        return group != null;
    }

    public async Task<int> SameBookingsCount(Entities.Booking booking)
    {
        var group = await GetBookingGroup(booking);
        return group?.CurrentBookingCount() ?? 0;
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
            throw DomainRuleException.NotFound("Не существует бронирования с таким индефекатором");

        if (booking.Status == BookingStatus.Canceled)
            throw new DomainRuleException($"Бронирования уже в статусе {booking.Status.GetDisplayName()}");

        if (booking.BookingGroup?.IsStarted() == true)
            throw new DomainRuleException($"Бронирования не может быть отменено !");
        var bookingGroup = booking.BookingGroup;
        booking.CancelBooking();
        if (bookingGroup != null)
        {
            var bookingLimit = await GetBookingLimit(booking);
            bookingGroup.SetStatus(bookingLimit);
        }

        await bookingRepository.Update(booking);
    }

    public async Task PaidBooking(Guid bookingId, decimal amount)
    {
        await CheckBookingPayable(bookingId, amount);
        var booking = await bookingRepository.FindFirst(new GetBookingById(bookingId));
        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            booking!.PaidBooking();
            booking.BookingGroup?.SetStarted();
            await bookingRepository.Update(booking);
            transactionScope.Complete();
        }
    }

    public async Task CheckBookingPayable(Guid bookingId, decimal amount)
    {
        var booking = await bookingRepository.FindFirst(new GetBookingById(bookingId));
        if (booking == null)
            throw DomainRuleException.NotFound("Не существует бронирования с таким индефекатором");
        if (!booking.IsWaitingPayment())
            throw new DomainRuleException("Не правильный статус бронирования для оплаты !",
                (int)BookingExceptionStatus.InvalidPaymentStatus);
        if (booking.BookingType.CostInTiyn != amount)
        {
            throw new DomainRuleException("Не правильная сумма оплаты !",
                (int)BookingExceptionStatus.InvalidAmount);
        }
    }
}