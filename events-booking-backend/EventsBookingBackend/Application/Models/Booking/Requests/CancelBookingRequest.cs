namespace EventsBookingBackend.Application.Models.Booking.Requests;

public class CancelBookingRequest
{
    public Guid BookingId { get; set; }
}