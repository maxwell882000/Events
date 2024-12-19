using EventsBookingBackend.Domain.Common.Repositories;
using EventsBookingBackend.Infrastructure.Notification.Telegram.Entities;

namespace EventsBookingBackend.Infrastructure.Notification.Telegram.Repositories;

public interface ITelegramRepository : IBaseRepository<TelegramUser>;