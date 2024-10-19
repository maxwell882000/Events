using EventsBookingBackend.Application.Models.Review.Requests;
using EventsBookingBackend.Application.Models.Review.Responses;

namespace EventsBookingBackend.Application.Services.Review;

public interface IReviewService
{
    public Task<GetReviewsByEventResponse> GetReviewsByEvent(GetReviewsByEventRequest request);
    public Task<CreateReviewResponse> CreateReview(CreateReviewRequest request);
    public Task<UpdateReviewResponse> UpdateReview(UpdateReviewRequest request);
}