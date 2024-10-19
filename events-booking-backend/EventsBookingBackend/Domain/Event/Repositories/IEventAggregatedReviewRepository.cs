using EventsBookingBackend.Domain.Common.Repositories;
using EventsBookingBackend.Domain.Event.Entities;

namespace EventsBookingBackend.Domain.Event.Repositories;

public interface IEventAggregatedReviewRepository : IBaseRepository<EventAggregatedReview>;