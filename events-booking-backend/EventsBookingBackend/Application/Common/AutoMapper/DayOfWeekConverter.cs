using AutoMapper;

namespace EventsBookingBackend.Application.Common.AutoMapper;

public class DayOfWeekConverter : ITypeConverter<DayOfWeek?, string?>, ITypeConverter<DayOfWeek, string>
{
    public string? Convert(DayOfWeek? source, string? destination, ResolutionContext context)
    {
        if (source != null)
        {
            return Convert(source.Value);
        }

        return null;
    }

    public string Convert(DayOfWeek source, string destination, ResolutionContext context)
    {
        return Convert(source);
    }

    private string Convert(DayOfWeek source)
    {
        DateTime date = new DateTime(2024, 9, 1).AddDays((int)source);
        var dayOfWeekStr = date.ToString("dddd");
        return Char.ToUpper(dayOfWeekStr[0]) + dayOfWeekStr.Substring(1);
    }
}