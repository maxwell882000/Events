using EventsBookingBackend.Domain.Category.Entities;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.Category;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Infrastructure.Persistence.DbContexts;

public class CategoryDbContext(DbContextOptions<CategoryDbContext> options) : BaseDbContext<CategoryDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("categories");
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
    }

    public DbSet<Category> Categories { get; init; }
}