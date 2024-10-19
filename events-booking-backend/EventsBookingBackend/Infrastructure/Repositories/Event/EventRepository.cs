using EventsBookingBackend.Domain.Event.Repositories;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using EventsBookingBackend.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingBackend.Infrastructure.Repositories.Event;

public class EventRepository(EventDbContext context)
    : BaseRepository<Domain.Event.Entities.Event, EventDbContext>(context), IEventRepository;