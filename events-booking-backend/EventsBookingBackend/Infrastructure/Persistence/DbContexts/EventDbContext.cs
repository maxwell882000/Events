using EventsBookingBackend.Domain.Event.Entities;
using EventsBookingBackend.Domain.User.Entities;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.Event;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Infrastructure.Persistence.DbContexts;

public class EventDbContext(DbContextOptions<EventDbContext> options) : BaseDbContext<EventDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("events");
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new EventLikedConfiguration());
    }

    public DbSet<Event> Events { get; init; }
    public DbSet<EventAggregatedReview> EventAggregatedReviews { get; init; }
    public DbSet<LikedEvent> LikedEvents { get; init; }
}