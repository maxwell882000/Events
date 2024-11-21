using EventsBookingBackend.Application.Common;
using EventsBookingBackend.Application.Models.Common;
using EventsBookingBackend.Application.Models.User.Responses;
using EventsBookingBackend.Domain.File.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventsBookingBackend.Api.Controllers;

public class FileController(IFileDomainService fileDomainService) : AppBaseController
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<ActionResult<FileDto>> GetUserProfile(IFormFile file)
    {
        var path = await fileDomainService.UploadFileAsync(file);
        return Ok(new FileDto()
        {
            Path = path,
        });
    }
}