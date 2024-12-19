using Telegram.Bot.Types;

namespace EventsBookingBackend.Infrastructure.Notification.Telegram.Services;

public interface ITelegramService
{
    Task CreateUser(Update update);
    Task Notify(string message);
}