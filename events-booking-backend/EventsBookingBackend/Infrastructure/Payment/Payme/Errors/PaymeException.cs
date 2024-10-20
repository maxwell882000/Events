namespace EventsBookingBackend.Infrastructure.Payment.Payme.Errors;

public class PaymeException : Exception
{
    public int? Id { get; set; }
    public PaymeMessageException Error { get; set; }
}