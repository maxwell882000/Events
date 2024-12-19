using EventsBookingBackend.Domain.Booking.Events;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Booking.Specifications;
using EventsBookingBackend.Infrastructure.Notification.Telegram.Services;
using MassTransit;
using Newtonsoft.Json;
using Telegram.Bot;

namespace EventsBookingJob.Consumers.Notification;

public class CreateBookingNotificationConsumer(ITelegramService telegramService, IBookingRepository bookingRepository)
    : IConsumer<BookingCreatedEvent>
{
    public async Task Consume(ConsumeContext<BookingCreatedEvent> context)
    {
        var booking = await bookingRepository.FindFirst(new GetBookingById(context.Message.Id));
        await telegramService.Notify(
            @$"Новое бронирование: {booking.Id}
Статус группы: {booking.BookingGroup?.Status}
Количество людей: {booking.BookingGroup?.CurrentBookingCount()}
Статус бронирования: {booking.Status}
Тип бронирования: {booking.BookingType.Label}
Названия события: {booking.Event.Name}
Данные о событие: {string.Join(", ", booking.BookingOptions.Select(e => e.BookingOptionValue.Value))}
            ");
    }
}