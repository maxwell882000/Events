namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Responses;

public class CheckPerformTransactionResponse
{
    public bool Allow { get; set; }

    public static CheckPerformTransactionResponse AllowRequest()
    {
        return new CheckPerformTransactionResponse()
        {
            Allow = true
        };
    }
}