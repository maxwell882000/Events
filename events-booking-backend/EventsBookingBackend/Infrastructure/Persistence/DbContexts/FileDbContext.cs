using EventsBookingBackend.Infrastructure.Persistence.Configurations.File;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Infrastructure.Persistence.DbContexts;

public class FileDbContext(DbContextOptions<FileDbContext> options) : BaseDbContext<FileDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("files");
        modelBuilder.ApplyConfiguration(new FileConfiguration());
    }
}