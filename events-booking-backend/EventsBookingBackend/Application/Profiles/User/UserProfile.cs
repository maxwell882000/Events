using AutoMapper;
using EventsBookingBackend.Application.Models.User.Responses;
using EventsBookingBackend.Domain.Booking.ValueObjects;

namespace EventsBookingBackend.Application.Profiles.User;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Domain.User.Entities.User, GetUserProfileResponse>();

        CreateMap<Domain.Booking.Entities.Booking, GetUserBookedEventResponse>()
            .ForMember(e => e.Type, opt
                => opt.MapFrom(src => src.BookingType.Label))
            .ForMember(e => e.Name, opt
                => opt.MapFrom(src => src.Event.Name))
            .ForMember(e => e.Cost, opt
                => opt.MapFrom(src => src.BookingType.Cost))
            .ForMember(e => e.Options,
                opt
                    => opt.MapFrom(e =>
                        string.Join(", ", e.BookingOptions.OrderBy(e => e.Option.Order)
                            .Select(e =>
                                e.Option.Type == BookingOptionType.DATE
                                    ? DateTime.Parse(e.BookingOptionValue.Value).ToString("dd/mm/yyyy")
                                    : e.BookingOptionValue.Value))));

        CreateMap<Domain.Event.Entities.Event, GetUserLikedEventResponse>().ForMember(e => e.WorkDay,
                opt => opt.MapFrom(e => e.Building.WorkDay!.Day))
            .ForMember(e => e.NextTime,
                opt => opt.MapFrom(e => e.Building.NextTime))
            .ForMember(e => e.IsOpen,
                opt => opt.MapFrom(e => e.Building.IsOpen))
            .ForMember(e => e.Image,
                opt => opt.MapFrom(e => e.PreviewImage))
            .ForMember(e => e.Address,
                opt => opt.MapFrom(e => e.Building.Address))
            .ForMember(e => e.Coordinates,
                opt => opt.MapFrom(e => e.Building.LatLong))
            .ForMember(e => e.Mark,
                opt => opt.MapFrom(e => e.AggregatedReviews!.OverallMark))
            .ForMember(e => e.ReviewCount,
                opt => opt.MapFrom(e => e.AggregatedReviews!.ReviewCount))
            .ForMember(e => e.CategoryId,
                opt => opt.MapFrom(e => e.CategoryId));
    }
}