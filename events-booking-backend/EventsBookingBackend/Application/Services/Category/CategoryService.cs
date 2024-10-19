using AutoMapper;
using EventsBookingBackend.Application.Models.Category.Responses;
using EventsBookingBackend.Domain.Category.Repositories;
using EventsBookingBackend.Domain.Category.Specifications;

namespace EventsBookingBackend.Application.Services.Category;

public class CategoryService(ICategoriesRepository categoriesRepository, IMapper mapper) : ICategoryService
{
    public async Task<List<GetAllCategoriesResponse>> GetAllCategories()
    {
        return mapper.Map<List<GetAllCategoriesResponse>>(await categoriesRepository.FindAll(new GetAllCategories()));
    }
}