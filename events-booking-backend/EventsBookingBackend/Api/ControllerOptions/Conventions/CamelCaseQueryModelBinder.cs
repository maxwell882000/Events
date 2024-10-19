using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventsBookingBackend.Api.ControllerOptions.Conventions;

public class CamelCaseQueryModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var modelName = bindingContext.ModelName;
        var camelCaseName = char.ToLowerInvariant(modelName[0]) + modelName.Substring(1);

        var valueProviderResult = bindingContext.ValueProvider.GetValue(camelCaseName);

        if (valueProviderResult != ValueProviderResult.None)
        {
            bindingContext.Result = ModelBindingResult.Success(valueProviderResult.FirstValue);
        }
        else
        {
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}