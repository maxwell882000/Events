using EventsBookingBackend.Infrastructure.Notification.Telegram.Options;
using EventsBookingBackend.Infrastructure.Notification.Telegram.Repositories;
using EventsBookingBackend.Infrastructure.Notification.Telegram.Services;
using Telegram.Bot;

namespace EventsBookingBackend.Infrastructure.Notification.Telegram.Extensions;

public static class TelegramExtesions
{
    public static void AddTelegram(this IServiceCollection services, IConfiguration configuration)
    {
        var message = configuration[TelegramOption.ApiKey];
        services.AddSingleton<ITelegramBotClient>(x =>
            new TelegramBotClient(configuration[TelegramOption.ApiKey] ?? string.Empty));
        services.ConfigureTelegramBotMvc();

        services.AddTransient<ITelegramService, TelegramService>();
        services.AddTransient<ITelegramRepository, TelegramRepository>();
    }

    public static async Task UseWebhook(this IApplicationBuilder app, IConfiguration configuration)
    {
        var d = configuration[TelegramOption.WebhookKey];
        using var scope = app.ApplicationServices.CreateScope();
        var bot = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
        await bot.SetWebhook(configuration[TelegramOption.WebhookKey] ?? string.Empty);
    }
}