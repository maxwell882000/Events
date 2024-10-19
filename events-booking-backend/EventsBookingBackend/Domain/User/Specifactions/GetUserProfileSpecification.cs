using EventsBookingBackend.Domain.Common.Specifications;

namespace EventsBookingBackend.Domain.User.Specifactions;

public class GetUserProfileSpecification(Guid userId) : ISpecification<Entities.User>
{
    public IQueryable<Entities.User> Apply(IQueryable<Entities.User> query)
    {
        return query.Where(e => e.Id == userId);
    }
}