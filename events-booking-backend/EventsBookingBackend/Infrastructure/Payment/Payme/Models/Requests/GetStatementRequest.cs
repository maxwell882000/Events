namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Requests;

public class GetStatementRequest
{
    public long From { get; set; }
    public long To { get; set; }
}