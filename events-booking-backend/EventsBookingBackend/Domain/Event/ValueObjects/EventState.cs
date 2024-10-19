namespace EventsBookingBackend.Domain.Event.ValueObjects;

public class EventState
{
    public bool IsReservable { get; set; }
    public bool IsActive { get; set; }
}