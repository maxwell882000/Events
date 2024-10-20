using EventsBookingBackend.Infrastructure.Payment.Payme.Errors;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Dto;

public class PaymeMessageErrorDto
{
    public PaymeErrors Code { get; set; }
    public MessageDto? Message { get; set; }
}