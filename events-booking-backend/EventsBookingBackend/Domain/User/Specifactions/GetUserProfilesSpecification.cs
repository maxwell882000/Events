using EventsBookingBackend.Domain.Common.Specifications;

namespace EventsBookingBackend.Domain.User.Specifactions;

public class GetUserProfilesSpecification(List<Guid> userIds) : ISpecification<Entities.User>
{
    public IQueryable<Entities.User> Apply(IQueryable<Entities.User> query)
    {
        return query.Where(e => userIds.Contains(e.Id));
    }
}