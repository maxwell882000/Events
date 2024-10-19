using EventsBookingBackend.Application.Models.Category.Responses;

namespace EventsBookingBackend.Application.Services.Category;

public interface ICategoryService
{
    public Task<List<GetAllCategoriesResponse>> GetAllCategories();
}