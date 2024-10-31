using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using EventsBookingBackend.Infrastructure.Payment.Payme.Option;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;


namespace EventsBookingBackend.Api.ControllerOptions.Auth;

public class BasicAuthenticationHandler<T>(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    IOptions<T> option,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder) where T : PaymentOption
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var log = logger?.CreateLogger<BasicAuthenticationHandler<T>>();
        if (!Request.Headers.ContainsKey("Authorization"))
            return await CreateFailureResponse("Missing Authorization Header");

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
            var username = credentials[0];
            var password = credentials[1];
            log.LogInformation(
                $"Username {username}. Password {password}. In options  Username {option.Value.Paycom}. Password {option.Value.Password}");

            // Validate credentials here (e.g., check against a database or predefined credentials)
            if (username != option.Value.Paycom && password != option.Value.Password)
            {
                return await CreateFailureResponse("Invalid Username or Password");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, username),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        catch
        {
            return await CreateFailureResponse("Invalid Authorization Header");
        }
    }

    protected virtual Task<AuthenticateResult> CreateFailureResponse(string message)
    {
        return Task.FromResult<AuthenticateResult>(AuthenticateResult.Fail(message));
    }
}