using EventsBookingBackend.Infrastructure.Notification.Telegram.Entities;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.Telegram;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Infrastructure.Persistence.DbContexts;

public class TelegramDbContext(DbContextOptions<TelegramDbContext> options) : BaseDbContext<TelegramDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("telegram");
        modelBuilder.ApplyConfiguration(new TelegramUserConfiguration());
    }

    public DbSet<TelegramUser> TelegramUsers { get; set; }
}