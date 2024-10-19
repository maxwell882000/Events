using EventsBookingBackend.Domain.File.Repositories;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using EventsBookingBackend.Infrastructure.Repositories.Common;

namespace EventsBookingBackend.Infrastructure.Repositories.File;

public class FileDbDbRepository(FileDbContext context)
    : BaseRepository<Domain.File.Entities.File, FileDbContext>(context), IFileDbRepository;