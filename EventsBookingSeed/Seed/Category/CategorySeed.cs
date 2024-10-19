using EventsBookingBackend.Domain.Category.Repositories;
using Seeds.Seed.Common;

namespace Seeds.Seed.Category;

public class CategorySeed(ICategoriesRepository categoriesRepository, ILogger<CategorySeed> logger)
    : BaseSeed<List<EventsBookingBackend.Domain.Category.Entities.Category>>("seed_category.json")
{
    protected override async Task SeedAsync(List<EventsBookingBackend.Domain.Category.Entities.Category> model)
    {
        var isCategorySeeded = await categoriesRepository.FindFirst();

        if (isCategorySeeded != null)
        {
            logger.LogInformation("CategorySeed was seeded already");
            return;
        }

        foreach (var item in model)
        {
            await categoriesRepository.Create(item);
        }
    }
}