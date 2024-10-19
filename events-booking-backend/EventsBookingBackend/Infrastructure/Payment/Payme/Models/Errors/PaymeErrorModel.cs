using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Responses;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Errors;

public class PaymeErrorModel : Exception
{
    public int? Id { get; set; }
    public PaymeErrorResponse Error { get; set; }
}