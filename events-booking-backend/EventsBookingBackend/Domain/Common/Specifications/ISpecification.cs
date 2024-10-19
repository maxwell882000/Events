using EventsBookingBackend.Domain.Common.Entities;

namespace EventsBookingBackend.Domain.Common.Specifications;

public interface ISpecification<T> where T : BaseEntity
{
    public IQueryable<T> Apply(IQueryable<T> query);
}