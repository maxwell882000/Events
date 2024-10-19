using EventsBookingBackend.Application.Common.Exceptions;
using EventsBookingBackend.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventsBookingBackend.Api.ControllerOptions.Filters;

public class ValidationResponseFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            throw new AppValidationException("Ошибка валидации", errors: context
                .ModelState
                .ToDictionary(e => e.Key.ToCamelCase(),
                    e => e.Value!.Errors.Select(e => e.ErrorMessage).FirstOrDefault() ?? ""
                ));
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}