using EventsBookingBackend.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserEntity = EventsBookingBackend.Domain.User.Entities.User;

namespace EventsBookingBackend.Infrastructure.Persistence.Configurations.User;

public class UserConfiguration : BaseEntityConfiguration<UserEntity>
{
    public override void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        base.Configure(builder);
        builder.HasIndex(e => e.Name);

        builder.OwnsOne(e => e.Avatar);
    }
}