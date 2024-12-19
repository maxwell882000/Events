using EventsBookingBackend.Infrastructure.Notification.Telegram.Entities;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsBookingBackend.Infrastructure.Persistence.Configurations.Telegram;

public class TelegramUserConfiguration : BaseEntityConfiguration<TelegramUser>
{
    public override void Configure(EntityTypeBuilder<TelegramUser> builder)
    {
        base.Configure(builder);
        builder.HasIndex(e => e.ChatId).IsUnique();
    }
}