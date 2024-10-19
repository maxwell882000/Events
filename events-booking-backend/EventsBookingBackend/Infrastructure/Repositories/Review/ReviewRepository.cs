using EventsBookingBackend.Domain.Review.Repositories;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using EventsBookingBackend.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Infrastructure.Repositories.Review;

public class ReviewRepository(ReviewDbContext context)
    : BaseRepository<Domain.Review.Entities.Review, ReviewDbContext>(context), IReviewRepository;