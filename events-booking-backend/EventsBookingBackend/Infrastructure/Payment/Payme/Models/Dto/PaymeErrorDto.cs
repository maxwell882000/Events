using EventsBookingBackend.Infrastructure.Payment.Payme.Errors;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Dto;

public class PaymeErrorDto
{
    public int? Id { get; set; }
    public PaymeMessageErrorDto Error { get; set; }
}