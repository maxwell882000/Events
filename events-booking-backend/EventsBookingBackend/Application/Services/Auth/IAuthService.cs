using System.Security.Claims;
using EventsBookingBackend.Application.Models.Auth.Dto;
using EventsBookingBackend.Application.Models.Auth.Requests;
using EventsBookingBackend.Application.Models.Auth.Responses;

namespace EventsBookingBackend.Application.Services.Auth;

public interface IAuthService
{
    public Task<ClaimsPrincipal> Register(AuthRegisterRequest request);
    public Task<ClaimsPrincipal> Login(AuthLoginRequest request);
    public Task<AuthDto?> GetCurrentAuthUser();
    public Guid? GetCurrentAuthUserId();
}