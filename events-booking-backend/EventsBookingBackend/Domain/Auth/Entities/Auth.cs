using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;

namespace EventsBookingBackend.Domain.Auth.Entities;

public class Auth : IdentityUser<Guid>
{
    public ClaimsPrincipal GetPrincipal => new(
        new ClaimsIdentity(
            new[]
            {
                new Claim(ClaimTypes.MobilePhone, UserName!),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
            },
            BearerTokenDefaults.AuthenticationScheme
        )
    );
}