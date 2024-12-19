using EventsBookingBackend.Domain.Common.Entities;

namespace EventsBookingBackend.Infrastructure.Notification.Telegram.Entities;

public class TelegramUser : BaseEntity
{
    public long ChatId { get; set; }
}