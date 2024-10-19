using EventsBookingBackend.Domain.Common.Specifications;

namespace EventsBookingBackend.Domain.Category.Specifications;

public class GetAllCategories : ISpecification<Entities.Category>
{
    public IQueryable<Entities.Category> Apply(IQueryable<Entities.Category> query)
    {
        return query.OrderBy(e => e.Order);
    }
}