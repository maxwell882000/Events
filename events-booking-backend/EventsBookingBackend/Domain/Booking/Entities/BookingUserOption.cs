using EventsBookingBackend.Domain.Booking.ValueObjects;
using EventsBookingBackend.Domain.Common.Entities;
using EventsBookingBackend.Domain.Common.Exceptions;

namespace EventsBookingBackend.Domain.Booking.Entities;

public class BookingUserOption : BaseEntity
{
    private BookingOptionValue _bookingOptionValue;
    public BookingOption Option { get; set; }

    public BookingOptionValue BookingOptionValue
    {
        get => _bookingOptionValue;
        set => _bookingOptionValue = value;
    }

    public Guid BookingId { get; set; }
    public Guid OptionId { get; set; }

    public void ValidateValue()
    {
        if (Option.Type == BookingOptionType.DATE && !DateTime.TryParse(BookingOptionValue.Value, out _))
        {
            throw new DomainRuleException(
                $"Указанное значение '{BookingOptionValue.Value}' не является допустимой датой.");
        }
    }
}