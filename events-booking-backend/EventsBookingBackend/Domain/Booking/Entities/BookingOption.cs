using EventsBookingBackend.Domain.Booking.ValueObjects;
using EventsBookingBackend.Domain.Common.Entities;

namespace EventsBookingBackend.Domain.Booking.Entities;

public class BookingOption : BaseEntity
{
    private List<BookingOptionValue>? _bookingOptionValues;
    public string Label { get; set; }
    public int Order { get; set; }
    public BookingOptionType Type { get; set; }

    public List<BookingOptionValue>? BookingOptionValues
    {
        get => _bookingOptionValues.OrderBy(o => o.Order).ToList();
        set => _bookingOptionValues = value;
    }

    public BookingType BookingType { get; set; }
}