namespace EventsBookingBackend.Application.Models.Booking.Responses;

public class CreateBookingResponse
{
    public Guid BookingId { get; set; }
    public string PaymentUrl { get; set; }
}