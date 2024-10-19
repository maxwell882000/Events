using EventsBookingBackend.Domain.Review.Entities;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.Review;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Infrastructure.Persistence.DbContexts;

public class ReviewDbContext(DbContextOptions<ReviewDbContext> options) : BaseDbContext<ReviewDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("reviews");
        modelBuilder.ApplyConfiguration(new ReviewConfiguration());
    }

    public DbSet<Review> Reviews { get; set; }
}