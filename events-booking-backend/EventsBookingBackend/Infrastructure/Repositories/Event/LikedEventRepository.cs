using EventsBookingBackend.Domain.Event.Entities;
using EventsBookingBackend.Domain.Event.Repositories;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using EventsBookingBackend.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Infrastructure.Repositories.Event;

public class LikedEventRepository(EventDbContext context)
    : BaseRepository<LikedEvent, EventDbContext>(context), ILikedEventRepository
{
    private async Task Create(LikedEvent likedEvent)
    {
        await base.Create(likedEvent);
    }

    public async Task<bool> Upsert(LikedEvent likedEvent)
    {
        var isExists =
            await context.LikedEvents.CountAsync(e =>
                e.EventId == likedEvent.EventId && e.UserId == likedEvent.UserId) > 0;
        if (isExists)
        {
            await Delete(likedEvent);
            return false;
        }

        await Create(likedEvent);
        return true;
    }

    private async Task Delete(LikedEvent likedEvent)
    {
        await context.LikedEvents.Where(e => e.EventId == likedEvent.EventId && e.UserId == likedEvent.UserId)
            .ExecuteDeleteAsync();
    }
}