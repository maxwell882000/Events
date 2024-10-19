using EventsBookingBackend.Api.ControllerOptions.Auth;
using EventsBookingBackend.Api.ControllerOptions.Filters;
using EventsBookingBackend.Application.Common;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Requests;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Responses;
using EventsBookingBackend.Infrastructure.Payment.Payme.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsBookingBackend.Api.Controllers.Payments;

[Microsoft.AspNetCore.Components.Route("pay")]
[IpRestriction([
    "185.234.113.1",
    "185.234.113.2",
    "185.234.113.3",
    "185.234.113.4",
    "185.234.113.5",
    "185.234.113.6",
    "185.234.113.7",
    "185.234.113.8",
    "185.234.113.9",
    "185.234.113.10",
    "185.234.113.11",
    "185.234.113.12",
    "185.234.113.13",
    "185.234.113.14",
    "185.234.113.15"
])]
[ServiceFilter(typeof(PaymeExceptionFilter))]
[Authorize(AuthenticationSchemes = "Payme")]
public class PaymeController(IPaymeService paymeService) : AppBaseController
{
    [HttpPost]
    public async Task<ActionResult<PaymeSuccessResponse>> Pay([FromBody] PaymeRequest request)
    {
        return Ok(await paymeService.Pay(request));
    }
}