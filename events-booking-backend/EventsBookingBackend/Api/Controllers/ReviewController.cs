using EventsBookingBackend.Application.Common;
using EventsBookingBackend.Application.Models.Review.Requests;
using EventsBookingBackend.Application.Models.Review.Responses;
using EventsBookingBackend.Application.Models.User.Responses;
using EventsBookingBackend.Application.Services.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsBookingBackend.Api.Controllers;

public class ReviewController(IReviewService reviewService) : AppBaseController
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("get-reviews-by-event")]
    public async Task<ActionResult<GetReviewsByEventResponse>> GetReviewsByEvent(
        [FromQuery] GetReviewsByEventRequest request)
    {
        var reviews = await reviewService.GetReviewsByEvent(request);
        return Ok(reviews);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CreateReviewResponse>> CreateReview(
        [FromBody] CreateReviewRequest request)
    {
        var response = await reviewService.CreateReview(request);
        return Created(string.Empty, response);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<UpdateReviewResponse>> UpdateReview(
        [FromBody] UpdateReviewRequest request)
    {
        var response = await reviewService.UpdateReview(request);
        return Created(string.Empty, response);
    }
}