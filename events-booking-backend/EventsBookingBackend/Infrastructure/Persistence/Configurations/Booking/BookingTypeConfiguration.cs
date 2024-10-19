using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsBookingBackend.Infrastructure.Persistence.Configurations.Booking;

public class BookingTypeConfiguration : BaseEntityConfiguration<BookingType>
{
    public override void Configure(EntityTypeBuilder<BookingType> builder)
    {
        base.Configure(builder);
        builder.HasMany(e => e.BookingOptions)
            .WithOne(e => e.BookingType);
        builder.OwnsOne(e => e.Icon).WithOwner();
        builder.HasIndex(e => new { e.CategoryId, e.Order }).IsUnique();
    }
}