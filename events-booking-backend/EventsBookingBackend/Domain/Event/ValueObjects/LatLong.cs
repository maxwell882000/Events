using EventsBookingBackend.Domain.Common.ValueObjects;

namespace EventsBookingBackend.Domain.Event.ValueObjects;

public class LatLong : BaseValueObject
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}