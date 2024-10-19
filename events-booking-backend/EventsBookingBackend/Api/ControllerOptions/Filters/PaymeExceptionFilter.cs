using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventsBookingBackend.Api.ControllerOptions.Filters;

public class PaymeExceptionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();
        if (resultContext.Exception != null && resultContext.Exception is PaymeErrorModel paymeErrorModel)
        {
            resultContext.Result = new ObjectResult(paymeErrorModel)
            {
                StatusCode = StatusCodes.Status200OK
            };
            resultContext.ExceptionHandled = true;
        }
    }
}