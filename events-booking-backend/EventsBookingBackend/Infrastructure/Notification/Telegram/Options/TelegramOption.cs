namespace EventsBookingBackend.Infrastructure.Notification.Telegram.Options;

public class TelegramOption
{
    public string Webhook { get; set; }
    public string Api { get; set; }

    public static string WebhookKey => nameof(TelegramOption) + ":" + nameof(Webhook);
    public static string ApiKey => nameof(TelegramOption) + ":" + nameof(Api);

}