using AutoMapper;
using EventsBookingBackend.Domain.Booking.Events;

namespace EventsBookingBackend.Domain.Booking.Profiles;

public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Entities.Booking, BookingCreatedEvent>();
        CreateMap<Entities.Booking, BookingCanceledEvent>();
    }
}