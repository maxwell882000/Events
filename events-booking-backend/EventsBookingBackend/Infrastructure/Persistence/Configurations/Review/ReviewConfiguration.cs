using EventsBookingBackend.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewEntity = EventsBookingBackend.Domain.Review.Entities.Review;

namespace EventsBookingBackend.Infrastructure.Persistence.Configurations.Review;

public class ReviewConfiguration : BaseEntityConfiguration<ReviewEntity>
{
    public override void Configure(EntityTypeBuilder<ReviewEntity> builder)
    {
        base.Configure(builder);
        builder.OwnsMany(e => e.MediaFiles);
        builder.HasIndex(e => new { e.EventId, e.UserId }).IsUnique();
        builder.HasIndex(e => e.EventId);
    }
}