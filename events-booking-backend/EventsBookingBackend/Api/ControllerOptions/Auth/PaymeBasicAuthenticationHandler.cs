using System.Text.Encodings.Web;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Errors;
using EventsBookingBackend.Infrastructure.Payment.Payme.Option;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EventsBookingBackend.Api.ControllerOptions.Auth;

public class PaymeBasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    IOptions<PaymeOption> option,
    UrlEncoder encoder) : BasicAuthenticationHandler<PaymeOption>(options, logger, option, encoder)
{
    protected override async Task<AuthenticateResult> CreateFailureResponse(string message)
    {
        {
            if (!Context.Response.HasStarted)
            {
                string result;
                Context.Response.StatusCode = StatusCodes.Status200OK;
                result = JsonConvert.SerializeObject(new { code = PaymeErrors.InvalidAuthorization });
                Context.Response.ContentType = "application/json";
                await Context.Response.WriteAsync(result);
            }
            else
            {
                await Context.Response.WriteAsync(string.Empty);
            }

            return AuthenticateResult.NoResult();
        }
    }
}