using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.Repositories;
using Seeds.Dto;
using Seeds.Seed.Common;

namespace Seeds.Seed.Booking;

public class BookingSeed(
    IBookingTypeRepository bookingTypeRepository,
    IBookingLimitRepository bookingLimitRepository,
    ILogger<BookingSeed> logger) : BaseSeed<List<BookingSeedDto>>("seed_booking.json")
{
    protected override async Task SeedAsync(List<BookingSeedDto> model)
    {
        var existingBookingTypes = await bookingTypeRepository.FindFirst();
        if (existingBookingTypes != null)
        {
            logger.LogInformation("BookingSeed was seeded already");
            return;
        }

        foreach (var item in model)
        {
            await bookingTypeRepository.Create(item.BookingType);
            item.BookingLimit.BookingTypeId = item.BookingType.Id;
            await bookingLimitRepository.Create(item.BookingLimit);
        }
    }
}