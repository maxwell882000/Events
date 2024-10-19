using System.Globalization;
using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Booking.Services;
using EventsBookingBackend.Domain.Booking.ValueObjects;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using EventsBookingBackend.Infrastructure.Repositories.Booking;
using EventsBookingTests.Factory;
using Microsoft.Extensions.Configuration;

namespace EventsBookingTests.Booking;

public class BookingTest
{
    private readonly IBookingDomainService _bookingDomainService = BookingFactory.CreateBookingDomainService();

    [Test]
    public async Task Check()
    {
        DateTime date = new DateTime(2021, 1, 1).AddDays((int)DayOfWeek.Friday);
        var d = DateTime.Now.ToString("dddd dd yyyy");

    }

    [Test]
    public async Task TestCreateBooking()
    {
        await _bookingDomainService.CreateBooking(new EventsBookingBackend.Domain.Booking.Entities.Booking()
        {
            EventId = Guid.Parse("ae9f2862-0a90-4184-a29f-be5e9e3d7aa8"),
            UserId = Guid.NewGuid(),
            BookingOptions =
            [
                new BookingUserOption()
                {
                    OptionId = Guid.Parse("235250bd-8427-4543-9b06-c2c92e771563"),
                    BookingOptionValue = new BookingOptionValue()
                    {
                        Value = "17:00 - 19:00"
                    }
                },
                new BookingUserOption()
                {
                    OptionId = Guid.Parse("39e8d6e6-ffd3-46e6-8643-6c5660abad29"),
                    BookingOptionValue = new BookingOptionValue()
                    {
                        Value = "Понедельник - Среда"
                    }
                }
            ],
            BookingTypeId = Guid.Parse("b6496f7d-4edd-4756-b987-587bf621e863")
        });
    }
}