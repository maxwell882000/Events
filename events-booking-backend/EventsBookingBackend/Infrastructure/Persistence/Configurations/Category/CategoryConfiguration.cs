using EventsBookingBackend.Domain.Common.ValueObjects;
using EventsBookingBackend.Infrastructure.Persistence.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CategoryEntity = EventsBookingBackend.Domain.Category.Entities.Category;

namespace EventsBookingBackend.Infrastructure.Persistence.Configurations.Category
{
    public class CategoryConfiguration : BaseEntityConfiguration<CategoryEntity>
    {
        public override void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            base.Configure(builder);
            builder.OwnsOne(e => e.Icon).WithOwner();
        }
    }
}