using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.ValueObjects;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsBookingBackend.Infrastructure.Persistence.Configurations.Booking;

public class BookingOptionConfiguration : BaseEntityConfiguration<BookingOption>
{
    public override void Configure(EntityTypeBuilder<BookingOption> builder)
    {
        base.Configure(builder);

        builder.OwnsMany(e => e.BookingOptionValues).WithOwner();
        builder.Property(e => e.Type).HasConversion<string>();
        builder.HasIndex("BookingTypeId", "Order").IsUnique();
    }
}