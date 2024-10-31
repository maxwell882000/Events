using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Booking.Services;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using EventsBookingBackend.Infrastructure.Repositories.Booking;
using EventsBookingTests.Static;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingTests.Factory;

public class BookingFactory
{
    public static IBookingRepository CreateBookingRepository(BookingDbContext context)
    {
        return new BookingRepository(context);
    }

    public static IBookingGroupRepository CreateBookingGroupRepository(BookingDbContext context)
    {
        return new BookingGroupRepository(context);
    }


    public static IBookingOptionRepository CreateBookingOptionRepository(BookingDbContext context)
    {
        return new BookingOptionRepository(context);
    }

    public static IBookingLimitRepository CreateBookingLimitRepository(BookingDbContext context)
    {
        return new BookingLimitRepository(context);
    }

    public static IBookingTypeRepository CreateBookingTypeRepository()
    {
        var context = new BookingDbContext(DbContextOptionsFactory<BookingDbContext>.Create()!);
        return new BookingTypeRepository(context);
    }

    public static IBookingDomainService CreateBookingDomainService()
    {
        var context = new BookingDbContext(DbContextOptionsFactory<BookingDbContext>.Create()!);
        return new BookDomainService(CreateBookingRepository(context),
            CreateBookingGroupRepository(context),
            CreateBookingOptionRepository(context),
            CreateBookingLimitRepository(context));
    }

    public static void Destroy(BookingDbContext context)
    {
        context.Database.EnsureDeleted(); // Ensures the database is deleted after tests
        context.Dispose();
    }
}