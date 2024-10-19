using EventsBookingBackend.Domain.Booking.Entities;

namespace Seeds.Dto;

public class BookingSeedDto
{
    public BookingType BookingType { get; set; }
    public BookingLimit? BookingLimit { get; set; }
}