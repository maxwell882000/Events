using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Requests;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json;

namespace EventsBookingBackend.Api.ControllerOptions.Types;

public class PaymeMethodConverter : JsonConverter<PaymeRequest.Methods>
{
    public override void WriteJson(JsonWriter writer, PaymeRequest.Methods value, JsonSerializer serializer)
    {
        // Serialize enum as string
        writer.WriteValue(value.GetDisplayName());
    }

    public override PaymeRequest.Methods ReadJson(JsonReader reader, Type objectType,
        PaymeRequest.Methods existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        // Read the value as string
        var enumString = reader.Value?.ToString();

        // Try to parse the string into the enum value
        if (Enum.TryParse(enumString, true, out PaymeRequest.Methods result))
        {
            return result;
        }

        throw new JsonSerializationException($"Unable to convert '{enumString}' to {typeof(PaymeRequest.Methods)}.");
    }
}