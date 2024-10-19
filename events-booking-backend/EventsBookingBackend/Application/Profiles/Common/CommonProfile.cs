using AutoMapper;
using EventsBookingBackend.Application.Common.AutoMapper;
using EventsBookingBackend.Application.Models.Common;
using EventsBookingBackend.Domain.Common.ValueObjects;

namespace EventsBookingBackend.Application.Profiles.Common;

public class CommonProfile : Profile
{
    public CommonProfile()
    {
        CreateMap<FileValueObject, FileDto>().ReverseMap();
        CreateMap<DayOfWeek?, string?>().ConvertUsing<DayOfWeekConverter>();
        CreateMap<DayOfWeek, string>().ConvertUsing<DayOfWeekConverter>();

    }
}