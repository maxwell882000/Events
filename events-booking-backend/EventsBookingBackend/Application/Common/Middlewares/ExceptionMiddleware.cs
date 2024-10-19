using EventsBookingBackend.Application.Common.Exceptions;
using EventsBookingBackend.Application.Models.Common;
using EventsBookingBackend.Domain.Common.Exceptions;

namespace EventsBookingBackend.Application.Common.Middlewares;

public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (AppValidationException ex)
        {
            logger.LogError($"App Validation Message: {ex.Message}\n Stack: {ex.StackTrace}");
            await WriteError(context, StatusCodes.Status400BadRequest, new ErrorModel
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Path = context.Request.Path.ToString(),
                Errors = ex.Errors
            });
        }
        catch (Exception ex) when (ex is AppValidationException or DomainRuleException)
        {
            logger.LogError($"App Validation Message: {ex.Message}\n Stack: {ex.StackTrace}");
            await WriteError(context, StatusCodes.Status400BadRequest, new ErrorModel
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Path = context.Request.Path.ToString()
            });
        }
        catch (Exception ex)
        {
            logger.LogError("Exceptions Message: " + ex.Message + "\n Stack: " + ex.StackTrace);
            await WriteError(context, StatusCodes.Status500InternalServerError, new ErrorModel()
                { Message = ex.Message, StackTrace = ex.StackTrace, Path = context.Request.Path.ToString() });
        }
    }

    private async Task WriteError(HttpContext context, int statusCode, ErrorModel error)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(error);
    }
}