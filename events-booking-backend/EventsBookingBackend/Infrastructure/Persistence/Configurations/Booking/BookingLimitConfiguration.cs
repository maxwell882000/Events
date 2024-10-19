using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsBookingBackend.Infrastructure.Persistence.Configurations.Booking;

public class BookingLimitConfiguration : BaseEntityConfiguration<BookingLimit>
{
    public override void Configure(EntityTypeBuilder<BookingLimit> builder)
    {
        base.Configure(builder);
        builder.HasOne<BookingType>()
            .WithMany()
            .HasForeignKey(x => x.BookingTypeId);
    }
}