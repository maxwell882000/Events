using EventsBookingBackend.Domain.Event.Entities;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsBookingBackend.Infrastructure.Persistence.Configurations.Event;

public class EventLikedConfiguration : BaseEntityConfiguration<LikedEvent>
{
    public override void Configure(EntityTypeBuilder<LikedEvent> builder)
    {
        base.Configure(builder);
        builder.HasOne<Domain.Event.Entities.Event>(e => e.Event)
            .WithMany(e => e.LikedEvents)
            .HasForeignKey(e => e.EventId);
    }
}