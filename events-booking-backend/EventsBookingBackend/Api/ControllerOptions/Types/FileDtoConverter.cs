using EventsBookingBackend.Api.Options;
using EventsBookingBackend.Application.Models.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EventsBookingBackend.Api.ControllerOptions.Types;

public class FileDtoConverter(IOptions<HostOption> options) : JsonConverter<FileDto>
{
    public override void WriteJson(JsonWriter writer, FileDto? value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("path");
        writer.WriteValue(value?.Path == null ? null : options.Value.BaseUrl + value?.Path);
        writer.WriteEndObject();
    }

    public override FileDto? ReadJson(JsonReader reader, Type objectType, FileDto? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        return serializer.Deserialize<FileDto>(reader);
    }
}