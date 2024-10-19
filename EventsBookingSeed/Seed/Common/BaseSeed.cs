using System.Text.Json;
using JsonNamingPolicy = System.Text.Json.JsonNamingPolicy;
using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;

namespace Seeds.Seed.Common;

public abstract class BaseSeed<T>(string jsonName) : ISeed
{
    public async Task SeedAsync()
    {
        string jsonString = await File.ReadAllTextAsync($"jsons/{jsonName}");

        var model = JsonSerializer.Deserialize<T>(jsonString, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        if (model != null) await SeedAsync(model);
    }

    protected abstract Task SeedAsync(T model);
}