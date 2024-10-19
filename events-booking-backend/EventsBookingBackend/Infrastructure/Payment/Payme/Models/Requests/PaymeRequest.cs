namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Requests;

public class PaymeRequest
{
    public int? Id { get; set; }
    public object Params { get; set; }
    public Methods? Method { get; set; }

    public enum Methods
    {
        PerformTransaction,
        CreateTransaction,
        CheckPerformTransaction,
        CancelTransaction,
        CheckTransaction,
        GetStatement,
        SetFiscalData,
    }
}