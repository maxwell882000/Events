using EventsBookingBackend.Api.ControllerOptions.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EventsBookingBackend.Shared.Json;

public static class PaymeJsonSettings
{
    public static JsonSerializerSettings SnakeCase()
    {
        return new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            Converters = new List<JsonConverter>()
            {
                new AccountConverter()
            }
        };
    }
}