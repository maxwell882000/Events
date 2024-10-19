using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Common.Repositories;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using EventsBookingBackend.Infrastructure.Repositories.Common;

namespace EventsBookingBackend.Infrastructure.Repositories.Booking;

public class BookingOptionRepository(BookingDbContext context)
    : BaseRepository<BookingOption, BookingDbContext>(context), IBookingOptionRepository;