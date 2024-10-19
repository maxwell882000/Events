using EventsBookingBackend.Domain.User.Repositories;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using EventsBookingBackend.Infrastructure.Repositories.Common;

namespace EventsBookingBackend.Infrastructure.Repositories.User;

public class UserRepository(UserDbContext context)
    : BaseRepository<Domain.User.Entities.User, UserDbContext>(context), IUserRepository;