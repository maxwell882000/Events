using EventsBookingBackend.Application.Common;
using EventsBookingBackend.Application.Models.Category.Responses;
using EventsBookingBackend.Application.Models.Event.Responses;
using EventsBookingBackend.Application.Services.Category;
using Microsoft.AspNetCore.Mvc;

namespace EventsBookingBackend.Api.Controllers;

public class CategoriesController(ICategoryService categoryService) : AppBaseController
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("get-all-categories")]
    public async Task<ActionResult<IList<GetAllCategoriesResponse>>> GetAllCategories()
    {
        return Ok(await categoryService.GetAllCategories());
    }
}