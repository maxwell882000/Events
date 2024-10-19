using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.ValueObjects;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsBookingBackend.Infrastructure.Persistence.Configurations.Booking;

public class BookingConfigurations : BaseEntityConfiguration<Domain.Booking.Entities.Booking>
{
    public override void Configure(EntityTypeBuilder<Domain.Booking.Entities.Booking> builder)
    {
        base.Configure(builder);

        builder.HasMany(e => e.BookingOptions)
            .WithOne()
            .HasForeignKey(e => e.BookingId);
        builder.HasOne(e => e.BookingType).WithMany().HasForeignKey(e => e.BookingTypeId);

        builder.Property(e => e.Status).HasConversion<string>();

        builder.HasOne(e => e.Event).WithMany().HasForeignKey(e => e.EventId);
    }
}