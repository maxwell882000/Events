using EventsBookingBackend.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsBookingBackend.Infrastructure.Persistence.Configurations.Base;

public class BaseValueObjectConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseValueObject
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasNoKey();
    }
}