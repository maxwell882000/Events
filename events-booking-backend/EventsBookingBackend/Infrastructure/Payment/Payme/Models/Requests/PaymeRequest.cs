using EventsBookingBackend.Shared.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Requests;

public class PaymeRequest
{
    public int? Id { get; set; }
    public JObject Params { get; set; }
    public Methods? Method { get; set; }

    public enum Methods
    {
        PerformTransaction,
        CreateTransaction,
        CheckPerformTransaction,
        CancelTransaction,
        CheckTransaction,
        GetStatement,
    }

    public object P => Method switch
    {
        Methods.PerformTransaction =>
            JsonConvert.DeserializeObject<PerformTransactionRequest>(Params.ToString(),
                JsonSettings.SnakeCase())!,
        Methods.CreateTransaction => JsonConvert.DeserializeObject<CreateTransactionRequest>(
            Params.ToString(),
            JsonSettings.SnakeCase())!,
        Methods.CheckPerformTransaction => JsonConvert.DeserializeObject<CheckPerformTransactionRequest>(
            Params.ToString(),
            JsonSettings.SnakeCase())!,
        Methods.CancelTransaction => JsonConvert.DeserializeObject<CancelTransactionRequest>(
            Params.ToString(),
            JsonSettings.SnakeCase())!,
        Methods.CheckTransaction => JsonConvert.DeserializeObject<CheckTransactionRequest>(
            Params.ToString(),
            JsonSettings.SnakeCase())!,
        Methods.GetStatement =>
            JsonConvert.DeserializeObject<GetStatementRequest>(
                Params.ToString(),
                JsonSettings.SnakeCase())!,
        _ => throw new InvalidOperationException("Unsupported method")
    };
}