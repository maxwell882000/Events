using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EventsBookingBackend.Shared.Json;

public static class JsonSettings
{
    public static JsonSerializerSettings SnakeCase()
    {
        return new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
    }
}