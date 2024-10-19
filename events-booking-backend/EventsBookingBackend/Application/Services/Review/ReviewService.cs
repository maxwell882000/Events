using AutoMapper;
using EventsBookingBackend.Application.Common.Exceptions;
using EventsBookingBackend.Application.Models.Common;
using EventsBookingBackend.Application.Models.Review.Requests;
using EventsBookingBackend.Application.Models.Review.Responses;
using EventsBookingBackend.Application.Services.Auth;
using EventsBookingBackend.Domain.Review.Repositories;
using EventsBookingBackend.Domain.Review.Specifications;
using EventsBookingBackend.Domain.User.Repositories;
using EventsBookingBackend.Domain.User.Specifactions;

namespace EventsBookingBackend.Application.Services.Review;

public class ReviewService(
    IUserRepository userRepository,
    IReviewRepository reviewRepository,
    IReviewAggregateRepository reviewAggregateRepository,
    IAuthService authService,
    IMapper mapper)
    : IReviewService
{
    public async Task<GetReviewsByEventResponse> GetReviewsByEvent(GetReviewsByEventRequest request)
    {
        var currentUserId = authService.GetCurrentAuthUserId();
        var reviews = await reviewRepository.FindAll(new GetEventReviews(request.EventId));
        var users = await userRepository.FindAll(
            new GetUserProfilesSpecification(reviews.Select(e => e.UserId).ToList()));
        var userReviewDtos = GetUserReviewDto(users, reviews, (user) => user.Id != currentUserId);
        var ownUserReview = GetUserReviewDto(users, reviews, (user) => user.Id == currentUserId).FirstOrDefault();
        var reviewAggregate =
            await reviewAggregateRepository.GetReviewAggregate(new GetEventReviews(request.EventId));

        return new GetReviewsByEventResponse()
        {
            EventId = request.EventId,
            Mark = reviewAggregate.Mark,
            ReviewCount = reviewAggregate.ReviewCount,
            UserReviews = userReviewDtos.ToList(),
            OwnReview = ownUserReview
        };
    }

    public async Task<CreateReviewResponse> CreateReview(CreateReviewRequest request)
    {
        var userId = (Guid)authService.GetCurrentAuthUserId()!;
        var existingReview =
            await reviewRepository.FindFirst(new GetUserReviewForEventSpecification(userId, (Guid)request.EventId!));
        if (existingReview is not null)
            throw new AppValidationException("Оценка была уже сделана");
        var review = mapper.Map<Domain.Review.Entities.Review>(request);
        review.UserId = userId;
        var result = await reviewRepository.Create(review);
        var reviewAggregate =
            await reviewAggregateRepository.GetReviewAggregate(new GetEventReviews(review.EventId));
        return new()
        {
            Id = result.Id, Mark = reviewAggregate.Mark,
            ReviewCount = reviewAggregate.ReviewCount,
        };
    }

    public async Task<UpdateReviewResponse> UpdateReview(UpdateReviewRequest request)
    {
        var userId = (Guid)authService.GetCurrentAuthUserId()!;
        var existingReview =
            await reviewRepository.FindFirst(new GetUserReviewForEventSpecification(userId, (Guid)request.EventId!));
        if (existingReview is null)
            throw new AppValidationException("Оценка не найдена");
        var review = mapper.Map(request, existingReview);
        var result = await reviewRepository.Update(review);
        var reviewAggregate =
            await reviewAggregateRepository.GetReviewAggregate(new GetEventReviews(review.EventId));
        return new()
        {
            Id = result.Id, Mark = reviewAggregate.Mark,
            ReviewCount = reviewAggregate.ReviewCount,
        };
    }


    private IEnumerable<GetReviewsByEventResponse.UserReviewDto> GetUserReviewDto(List<Domain.User.Entities.User> users,
        List<Domain.Review.Entities.Review> reviews, Func<Domain.User.Entities.User, bool> whereStatement)
    {
        return from user in users
            join review in reviews on user.Id equals review.UserId
            where whereStatement(user)
            select new GetReviewsByEventResponse.UserReviewDto()
            {
                Name = user.Name,
                Avatar = mapper.Map<FileDto>(user.Avatar),
                Mark = review.Mark,
                Comment = review.Comment,
                ReviewDate = review.CreatedAt
            };
    }
}