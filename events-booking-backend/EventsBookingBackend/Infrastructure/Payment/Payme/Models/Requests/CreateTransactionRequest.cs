using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Dto;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Requests;

public class CreateTransactionRequest
{
    public string Id { get; set; }
    public long Time { get; set; }
    public long Amount { get; set; }
    public AccountDto Account { get; set; }
}