using EventsBookingBackend.Application.Common;
using EventsBookingBackend.Infrastructure.Notification.Telegram.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace EventsBookingBackend.Api.Controllers.Notifications;

public class TelegramController(ITelegramService telegramService) : AppBaseController
{
    [HttpPost]
    public async Task<IActionResult> HandleUpdate([FromBody] Update update)
    {
        await telegramService.CreateUser(update);
        return Ok();
    }
}