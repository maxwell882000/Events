using AutoMapper;
using EventsBookingBackend.Application.Models.Category.Responses;

namespace EventsBookingBackend.Application.Profiles.Category;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Domain.Category.Entities.Category, GetAllCategoriesResponse>();
    }
}