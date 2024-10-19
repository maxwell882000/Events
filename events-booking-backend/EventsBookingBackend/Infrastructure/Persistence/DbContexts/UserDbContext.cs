using EventsBookingBackend.Domain.User.Entities;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.User;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Infrastructure.Persistence.DbContexts;

public class UserDbContext(DbContextOptions<UserDbContext> options) : BaseDbContext<UserDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

    public DbSet<User> Users { get; set; }
}