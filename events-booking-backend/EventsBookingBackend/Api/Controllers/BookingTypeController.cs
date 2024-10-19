using EventsBookingBackend.Application.Common;
using EventsBookingBackend.Application.Models.Booking.Requests;
using EventsBookingBackend.Application.Models.Booking.Responses;
using EventsBookingBackend.Application.Services.Book;
using Microsoft.AspNetCore.Mvc;

namespace EventsBookingBackend.Api.Controllers;

public class BookingTypeController(IBookingTypeService bookingTypeService) : AppBaseController
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<List<GetBookingTypeByCategoryResponse>>> GetBookingType(
        [FromQuery] GetBookingTypeByCategoryRequest request)
    {
        return Ok(await bookingTypeService.GetBookingType(request));
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<ActionResult<GetBookingTypeByCategoryResponse>> CreateBookingType(
        [FromBody] CreateBookingTypeRequest request)
    {
        var result = await bookingTypeService.CreateBookingType(request);
        return Created(string.Empty, result);
    }
}