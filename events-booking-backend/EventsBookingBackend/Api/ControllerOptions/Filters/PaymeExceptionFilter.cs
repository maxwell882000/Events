using AutoMapper;
using EventsBookingBackend.Infrastructure.Payment.Payme.Errors;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventsBookingBackend.Api.ControllerOptions.Filters;

public class PaymeExceptionFilter(IMapper mapper) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();
        if (resultContext.Exception != null && resultContext.Exception is PaymeException paymeErrorModel)
        {
            resultContext.Result = new ObjectResult(mapper.Map<PaymeErrorDto>(paymeErrorModel))
            {
                StatusCode = StatusCodes.Status200OK
            };
            resultContext.ExceptionHandled = true;
        }
    }
}