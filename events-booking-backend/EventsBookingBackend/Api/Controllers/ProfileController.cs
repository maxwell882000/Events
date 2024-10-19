using EventsBookingBackend.Application.Common;
using EventsBookingBackend.Application.Models.User.Responses;
using EventsBookingBackend.Application.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsBookingBackend.Api.Controllers;

[Authorize]
public class ProfileController(IUserService userService) : AppBaseController
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("get-user-profile")]
    public async Task<ActionResult<GetUserProfileResponse>> GetUserProfile()
    {
        var user = await userService.GetUserProfile();
        return Ok(user);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("get-user-booked-events")]
    public async Task<ActionResult<GetUserBookedEventResponse>> GetUserBookedEvents()
    {
        var bookings = await userService.GetUserBookedEvents();
        return Ok(bookings);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("get-user-liked-events")]
    public async Task<ActionResult<GetUserLikedEventResponse>> GetUserLikedEvents()
    {
        var likedEvents = await userService.GetUserLikedEvents();
        return Ok(likedEvents);
    }
}