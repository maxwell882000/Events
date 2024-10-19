using EventsBookingBackend.Domain.Common.Specifications;
using EventsBookingBackend.Domain.Review.Entities;
using EventsBookingBackend.Domain.Review.Repositories;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Infrastructure.Repositories.Review;

public class ReviewAggregateRepository(ReviewDbContext reviewDbContext) : IReviewAggregateRepository
{
    public async Task<ReviewAggregate> GetReviewAggregate(ISpecification<Domain.Review.Entities.Review>? specification)
    {
        var reviewsQuery = specification == null
            ? reviewDbContext.Reviews
            : specification.Apply(reviewDbContext.Reviews);
        var reviewCount = await reviewsQuery.CountAsync();
        var overallMark = await reviewsQuery.SumAsync(e => e.Mark);
        return new ReviewAggregate(overallMark, reviewCount);
    }
}