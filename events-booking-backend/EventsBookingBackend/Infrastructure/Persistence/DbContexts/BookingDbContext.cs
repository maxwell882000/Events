using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.Booking;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Infrastructure.Persistence.DbContexts;

public class BookingDbContext(DbContextOptions<BookingDbContext> options) : BaseDbContext<BookingDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("bookings");
        modelBuilder.ApplyConfiguration(new BookingOptionConfiguration());
        modelBuilder.ApplyConfiguration(new BookingTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BookingConfigurations());
        modelBuilder.ApplyConfiguration(new BookingUserOptionConfiguration());
        modelBuilder.ApplyConfiguration(new BookingLimitConfiguration());
        modelBuilder.ApplyConfiguration(new BookingEventConfiguration());
    }

    public DbSet<BookingOption> BookingOptions { get; init; }
    public DbSet<BookingType> BookingTypes { get; init; }
    public DbSet<Booking> Bookings { get; init; }
    public DbSet<BookingLimit> BookingLimits { get; init; }
    public DbSet<BookingUserOption> BookingUserOptions { get; init; }
    public DbSet<BookingEvent> BookingEvents { get; init; }
}