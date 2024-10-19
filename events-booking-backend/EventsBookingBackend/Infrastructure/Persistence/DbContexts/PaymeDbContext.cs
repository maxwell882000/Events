using EventsBookingBackend.Infrastructure.Payment.Payme.Entities;
using EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.Payme;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Infrastructure.Persistence.DbContexts;

public class PaymeDbContext(DbContextOptions<PaymeDbContext> options) : BaseDbContext<PaymeDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("payme");
        modelBuilder.ApplyConfiguration(new TransactionDetailConfiguration());
    }

    public DbSet<TransactionDetail<Account>> TransactionDetails { get; set; }
}