using AutoMapper;
using EventsBookingBackend.Application.Models.Auth.Dto;
using EventsBookingBackend.Application.Models.Auth.Requests;
using EventsBookingBackend.Domain.User.Entities;

namespace EventsBookingBackend.Application.Profiles.Auth;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<AuthRegisterRequest, Domain.Auth.Entities.Auth>()
            .ForMember(e => e.UserName, src => src.MapFrom(e => e.Phone))
            .ForMember(e => e.PhoneNumber, src => src.MapFrom(e => e.Phone));
        CreateMap<AuthRegisterRequest, Domain.User.Entities.User>();
        CreateMap<Domain.Auth.Entities.Auth, AuthDto>();
    }
}