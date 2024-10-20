using EventsBookingBackend.Api.ControllerOptions.Auth;
using EventsBookingBackend.Api.ControllerOptions.Filters;
using EventsBookingBackend.Application.Common;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Requests;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Responses;
using EventsBookingBackend.Infrastructure.Payment.Payme.Services;
using EventsBookingBackend.Shared.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace EventsBookingBackend.Api.Controllers.Payments;

// [IpRestriction([
//     "185.234.113.1",
//     "185.234.113.2",
//     "185.234.113.3",
//     "185.234.113.4",
//     "185.234.113.5",
//     "185.234.113.6",
//     "185.234.113.7",
//     "185.234.113.8",
//     "185.234.113.9",
//     "185.234.113.10",
//     "185.234.113.11",
//     "185.234.113.12",
//     "185.234.113.13",
//     "185.234.113.14",
//     "185.234.113.15"
// ])]
[ServiceFilter(typeof(PaymeExceptionFilter))]
[Authorize(AuthenticationSchemes = "Payme")]
[Route("pay")]
[ApiController]
public class PaymeController(IPaymeService paymeService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PaymeSuccessResponse>> Pay([FromBody] PaymeRequest request)
    {
        var response = await paymeService.Pay(request);


        var json = JsonConvert.SerializeObject(response, JsonSettings.SnakeCase());


        return new ContentResult
        {
            Content = json,
            ContentType = "application/json",
            StatusCode = 200
        };
    }
}