using Microsoft.AspNetCore.Mvc;

namespace EventsBookingBackend.Application.Common;

[ApiController]
[Produces("application/json", new string[] { })]
[Route("api/v1/[controller]")]
public class AppBaseController : ControllerBase;