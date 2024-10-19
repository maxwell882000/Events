using EventsBookingBackend.Domain.Common.Repositories;

namespace EventsBookingBackend.Domain.File.Repositories;

public interface IFileDbRepository : IBaseRepository<File.Entities.File>;