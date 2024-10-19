using EventsBookingBackend.Application.Common;
using EventsBookingBackend.Application.Models.Event.Requests;
using EventsBookingBackend.Application.Models.Event.Responses;
using EventsBookingBackend.Application.Services.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsBookingBackend.Api.Controllers;

public class EventController(IEventService eventService) : AppBaseController
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("get-all-events")]
    public async Task<ActionResult<IList<GetAllEventsResponse>>> GetAllEvents()
    {
        return Ok(await eventService.GetAllEvents());
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("get-event-detail")]
    public async Task<ActionResult<IList<GetEventDetailResponse>>> GetEventDetail(
        [FromQuery] GetEventDetailRequest request)
    {
        var detail = await eventService.GetEventDetail(request);
        return Ok(detail);
    }


    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [HttpPut("like")]
    [Authorize]
    public async Task<ActionResult<LikeEventResponse>> LikeEvent(
        [FromBody] LikeEventRequest request)
    {
        return Ok(await eventService.LikeEvent(request));
    }
}