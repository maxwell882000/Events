using AutoMapper;
using EventsBookingBackend.Application.Models.Review.Requests;
using EventsBookingBackend.Application.Models.Review.Responses;

namespace EventsBookingBackend.Application.Profiles.Review;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Domain.Review.Entities.Review, GetReviewsByEventResponse.UserReviewDto>();
        CreateMap<CreateReviewRequest, Domain.Review.Entities.Review>();
        CreateMap<UpdateReviewRequest, Domain.Review.Entities.Review>();

    }
}