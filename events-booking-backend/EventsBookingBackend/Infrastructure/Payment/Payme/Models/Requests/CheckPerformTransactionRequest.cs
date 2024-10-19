using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Dto;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Requests;

public class CheckPerformTransactionRequest
{
    public long? Amount { get; set; }
    public AccountDto? Account { get; set; }
}