using AutoMapper;
using EventsBookingBackend.Application.Models.Booking.Dto;
using EventsBookingBackend.Application.Models.Booking.Requests;
using EventsBookingBackend.Application.Models.Booking.Responses;
using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.ValueObjects;

namespace EventsBookingBackend.Application.Profiles.Booking;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<BookingType, GetBookingTypeByCategoryResponse>();
        CreateMap<CreateBookingRequest, Domain.Booking.Entities.Booking>();
        CreateMap<CreateBookingTypeRequest, BookingType>();
        CreateMap<BookingType, CreateBookingTypeResponse>();

        CreateMap<GetSameBookingsCountRequest, Domain.Booking.Entities.Booking>();
        CreateMap<BookingOption, BookingOptionDto>().ReverseMap();
        CreateMap<BookingOptionValue, BookingOptionValueDto>().ReverseMap();
        CreateMap<BookingUserOptionDto, BookingUserOption>().ReverseMap();
    }
}