using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventsBookingBackend.Api.ControllerOptions.Filters;

public class IpRestrictionAttribute : ActionFilterAttribute
{
    private readonly string[] _allowedIps;

    public IpRestrictionAttribute(string[] allowedIps)
    {
        _allowedIps = allowedIps;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
        if (remoteIp != null && !_allowedIps.Any(ip => IPAddress.Parse(ip).Equals(remoteIp)))
        {
            context.Result = new ForbidResult();
        }

        base.OnActionExecuting(context);
    }
}