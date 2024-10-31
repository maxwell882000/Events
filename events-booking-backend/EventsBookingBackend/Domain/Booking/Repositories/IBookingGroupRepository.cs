using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Common.Repositories;
using EventsBookingBackend.Domain.Common.Specifications;

namespace EventsBookingBackend.Domain.Booking.Repositories;

public interface IBookingGroupRepository : IBaseRepository<BookingGroup>;