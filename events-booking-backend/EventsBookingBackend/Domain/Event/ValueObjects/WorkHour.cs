using EventsBookingBackend.Domain.Common.ValueObjects;

namespace EventsBookingBackend.Domain.Event.ValueObjects;

public class WorkHour : BaseValueObject
{
    public DayOfWeek Day { get; set; }
    private int _fromHour;
    private int _toHour;

    public int FromHour
    {
        get => _fromHour;
        set
        {
            if (value < 0 || value > 23)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Hour must be between 0 and 23.");
            }

            _fromHour = value;
        }
    }

    public int ToHour
    {
        get => _toHour;
        set
        {
            if (value < 0 || value > 23)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Hour must be between 0 and 23.");
            }

            _toHour = value;
        }
    }
}