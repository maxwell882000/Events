using EventsBookingBackend.Infrastructure.Notification.Telegram.Entities;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using EventsBookingBackend.Infrastructure.Repositories.Common;

namespace EventsBookingBackend.Infrastructure.Notification.Telegram.Repositories;

public class TelegramRepository(TelegramDbContext paymeDbContext)
    : BaseRepository<TelegramUser, TelegramDbContext>(paymeDbContext), ITelegramRepository;