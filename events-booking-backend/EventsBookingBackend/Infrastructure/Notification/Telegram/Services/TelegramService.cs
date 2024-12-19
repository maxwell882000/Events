using EventsBookingBackend.Infrastructure.Notification.Telegram.Entities;
using EventsBookingBackend.Infrastructure.Notification.Telegram.Repositories;
using EventsBookingBackend.Infrastructure.Notification.Telegram.Spec;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EventsBookingBackend.Infrastructure.Notification.Telegram.Services;

public class TelegramService(ITelegramRepository telegramRepository, ITelegramBotClient telegramBotClient)
    : ITelegramService
{
    public async Task CreateUser(Update update)
    {
        if (update.Message?.From != null)
        {
            var user = await telegramRepository.FindFirst(new GetByChatIdSpec(update.Message.From.Id));
            if (user == null)
                await telegramRepository.Create(new TelegramUser()
                {
                    ChatId = update.Message.From.Id
                });
        }
    }

    public async Task Notify(string message)
    {
        var allUsers = await telegramRepository.FindAll();
        foreach (var user in allUsers)
        {
            await telegramBotClient.SendMessage(user.ChatId, message);
        }
    }
}