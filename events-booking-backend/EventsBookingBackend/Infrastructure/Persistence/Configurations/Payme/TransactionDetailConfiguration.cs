using EventsBookingBackend.Infrastructure.Payment.Payme.Entities;
using EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsBookingBackend.Infrastructure.Persistence.Configurations.Payme;

public class TransactionDetailConfiguration : BaseEntityConfiguration<TransactionDetail<Account>>
{
    public override void Configure(EntityTypeBuilder<TransactionDetail<Account>> builder)
    {
        base.Configure(builder);

        builder.HasIndex(e => e.PaymeId).IsUnique();
        builder.HasIndex(e => e.Time);
        builder.OwnsMany(e => e.Receivers).WithOwner();
        builder.OwnsOne(e => e.Account).WithOwner();
    }
}