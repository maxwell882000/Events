using EventsBookingBackend.Domain.Event.Entities;
using EventsBookingBackend.Domain.Event.Repositories;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using EventsBookingBackend.Infrastructure.Repositories.Common;

namespace EventsBookingBackend.Infrastructure.Repositories.Event;

public class EventAggregatedReviewRepository(EventDbContext context)
    : BaseRepository<EventAggregatedReview, EventDbContext>(context), IEventAggregatedReviewRepository;