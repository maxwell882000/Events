using System.Text.Encodings.Web;
using EventsBookingBackend.Infrastructure.Payment.Payme.Option;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace EventsBookingBackend.Api.ControllerOptions.Auth;

public class PaymeBasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    IOptions<PaymeOption> option,
    UrlEncoder encoder) : BasicAuthenticationHandler<PaymeOption>(options, logger, option, encoder);