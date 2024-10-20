using System.Text;
using System.Text.Encodings.Web;
using EventsBookingBackend.Infrastructure.Payment.Payme.Errors;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Dto;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Requests;
using EventsBookingBackend.Infrastructure.Payment.Payme.Option;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace EventsBookingBackend.Api.ControllerOptions.Auth;

public class PaymeBasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    IOptions<PaymeOption> option,
    UrlEncoder encoder) : BasicAuthenticationHandler<PaymeOption>(options, logger, option, encoder)
{
    protected override async Task<AuthenticateResult> CreateFailureResponse(string message)
    {
        var snakeCaseSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
        if (!Context.Response.HasStarted)
        {
            Request.EnableBuffering();
            using StreamReader reader = new(Request.Body, Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false, leaveOpen: false);
            var bodyAsString = await reader.ReadToEndAsync();
            // Reset the request body stream position to 0 so it can be read again
            Request.Body.Position = 0;
            var paymeRequest = JsonConvert.DeserializeObject<PaymeRequest>(bodyAsString, snakeCaseSettings);
            Context.Response.StatusCode = StatusCodes.Status200OK;
            string result = JsonConvert.SerializeObject(new PaymeErrorDto()
            {
                Id = paymeRequest?.Id,
                Error = new PaymeMessageErrorDto()
                {
                    Code = PaymeErrors.InvalidAuthorization,
                    Message = new MessageDto()
                    {
                        Uz = "Noto'g'ri avtorizatsiya",
                        Ru = "Недействительная авторизация",
                        En = "Invalid Authorization"
                    }
                }
            }, snakeCaseSettings);
            Context.Response.ContentType = "text/json; charset=UTF-8";
            await Context.Response.WriteAsync(result);
        }
        else
        {
            await Context.Response.WriteAsync(string.Empty);
        }

        return AuthenticateResult.NoResult();
    }
}