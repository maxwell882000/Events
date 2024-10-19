using System.Security.Claims;
using System.Transactions;
using AutoMapper;
using EventsBookingBackend.Application.Common.Exceptions;
using EventsBookingBackend.Application.Models.Auth.Dto;
using EventsBookingBackend.Application.Models.Auth.Requests;
using EventsBookingBackend.Domain.User.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace EventsBookingBackend.Application.Services.Auth;

public class AuthService(
    Microsoft.AspNetCore.Identity.UserManager<Domain.Auth.Entities.Auth?> userManager,
    IUserRepository userRepository,
    SignInManager<Domain.Auth.Entities.Auth> signInManager,
    ILogger<AuthService> logger,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper)
    : IAuthService
{
    public async Task<ClaimsPrincipal> Login(AuthLoginRequest request)
    {
        var result = await signInManager.PasswordSignInAsync(request.Phone!, request.Password!, false, false);

        if (result.Succeeded)
        {
            var auth = (await userManager.FindByNameAsync(request.Phone!))!;
            return auth.GetPrincipal;
        }

        throw new AppValidationException("Не правильный пароль или номер телефона");
    }

    public async Task<AuthDto?> GetCurrentAuthUser()
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user != null && user.Identity.IsAuthenticated)
            return mapper.Map<AuthDto>(await userManager.GetUserAsync(user));
        return null;
    }

    public Guid? GetCurrentAuthUserId()
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user != null && user.Identity?.IsAuthenticated == true)
        {
            return Guid.Parse(user.Identity.GetUserId());
        }

        return null;
    }


    public async Task<ClaimsPrincipal> Register(AuthRegisterRequest request)
    {
        if (request.Password != request.RepeatPassword)
            throw new AppValidationException("Пароли не совпадают");

        // using var transaction = await dbContext.Database.BeginTransactionAsync();
        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var auth = mapper.Map<Domain.Auth.Entities.Auth>(request);
            var result = await userManager.CreateAsync(auth, request.Password!);
            if (result.Succeeded)
            {
                var user = mapper.Map<Domain.User.Entities.User>(request);
                user.Id = auth.Id;
                await userRepository.Create(user);
                transactionScope.Complete();
                return auth.GetPrincipal;
            }

            throw new AppValidationException(result.Errors.Select(e => e.Description).FirstOrDefault() ??
                                             "Не удалось создать пользователя");
        }
    }
}