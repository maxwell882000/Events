using EventsBookingBackend.Domain.Common.Specifications;
using EventsBookingBackend.Infrastructure.Notification.Telegram.Entities;

namespace EventsBookingBackend.Infrastructure.Notification.Telegram.Spec;

public class GetByChatIdSpec(long chatId) : ISpecification<TelegramUser>
{
    public IQueryable<TelegramUser> Apply(IQueryable<TelegramUser> query)
    {
        return query.Where(e => e.ChatId == chatId);
    }
}