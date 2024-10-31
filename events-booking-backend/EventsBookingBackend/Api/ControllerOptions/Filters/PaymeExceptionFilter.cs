using System.Text;
using AutoMapper;
using EventsBookingBackend.Infrastructure.Payment.Payme.Errors;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Dto;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Requests;
using EventsBookingBackend.Shared.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace EventsBookingBackend.Api.ControllerOptions.Filters;

public class PaymeExceptionFilter(IMapper mapper) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.HttpContext.Request.EnableBuffering();
        using StreamReader reader = new(context.HttpContext.Request.Body, Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false, leaveOpen: false);
        var bodyAsString = await reader.ReadToEndAsync();
        // Reset the request body stream position to 0 so it can be read again
        context.HttpContext.Request.Body.Position = 0;
        var paymeRequest = JsonConvert.DeserializeObject<PaymeRequest>(bodyAsString, PaymeJsonSettings.SnakeCase());

        try
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

            if (resultContext.Exception != null &&
                resultContext.Exception is PaymeMessageException paymeMessageException)
            {
                resultContext.Result = new ObjectResult(new PaymeErrorDto()
                {
                    Id = paymeRequest.Id,
                    Error = mapper.Map<PaymeMessageErrorDto>(paymeMessageException)
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };
                resultContext.ExceptionHandled = true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}