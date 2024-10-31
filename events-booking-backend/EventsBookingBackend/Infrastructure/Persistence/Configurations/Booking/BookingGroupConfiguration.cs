using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsBookingBackend.Infrastructure.Persistence.Configurations.Booking;

public class BookingGroupConfiguration : BaseEntityConfiguration<BookingGroup>
{
    public override void Configure(EntityTypeBuilder<BookingGroup> builder)
    {
        builder.HasOne(e => e.BookingType).WithMany().HasForeignKey(e => e.BookingTypeId);
        builder.HasMany(e => e.Bookings).WithOne(e => e.BookingGroup).HasForeignKey(e => e.BookingGroupId);
        builder.Property(e => e.Status).HasConversion<string>();
        builder.HasIndex(e => new { e.BookingTypeId, e.EventId, e.UserOptions }).IsUnique();
    }
}