using EventsBookingBackend.Domain.Category.Repositories;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using EventsBookingBackend.Infrastructure.Repositories.Common;

namespace EventsBookingBackend.Infrastructure.Repositories.Category;

public class CategoryRepository(CategoryDbContext context)
    : BaseRepository<Domain.Category.Entities.Category, CategoryDbContext>(context),
        ICategoriesRepository;