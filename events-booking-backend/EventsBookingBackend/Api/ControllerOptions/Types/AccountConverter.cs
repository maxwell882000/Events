using EventsBookingBackend.Infrastructure.Payment.Payme.Errors;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EventsBookingBackend.Api.ControllerOptions.Types;

public class AccountConverter : JsonConverter<AccountDto>
{
    public override void WriteJson(JsonWriter writer, AccountDto? value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("booking_id");
        writer.WriteValue(value!.BookingId.ToString());
        writer.WriteEndObject();
    }

    public override AccountDto? ReadJson(JsonReader reader, Type objectType, AccountDto? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);
        if (Guid.TryParse(jsonObject["booking_id"].Value<string>(), out Guid bookingId))
            return new AccountDto
            {
                BookingId = bookingId,
            };
        return new AccountDto()
        {
            BookingId = Guid.Empty,
        };
    }
}