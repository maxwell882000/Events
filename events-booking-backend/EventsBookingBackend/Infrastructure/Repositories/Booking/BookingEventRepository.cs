using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using EventsBookingBackend.Infrastructure.Repositories.Common;

namespace EventsBookingBackend.Infrastructure.Repositories.Booking;

public class BookingEventRepository(BookingDbContext context)
    : BaseRepository<BookingEvent, BookingDbContext>(context), IBookingEventRepository;