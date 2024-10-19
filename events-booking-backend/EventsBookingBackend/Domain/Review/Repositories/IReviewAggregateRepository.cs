using EventsBookingBackend.Domain.Common.Specifications;
using EventsBookingBackend.Domain.Review.Entities;

namespace EventsBookingBackend.Domain.Review.Repositories;

public interface IReviewAggregateRepository
{
    public Task<ReviewAggregate> GetReviewAggregate(ISpecification<Entities.Review>? specification);
}