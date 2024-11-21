using System.Transactions;
using AutoMapper;
using EventsBookingBackend.Application.Common.Exceptions;
using EventsBookingBackend.Application.Models.Common;
using EventsBookingBackend.Application.Models.User.Requests;
using EventsBookingBackend.Application.Models.User.Responses;
using EventsBookingBackend.Application.Services.Auth;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Booking.Specifications;
using EventsBookingBackend.Domain.Booking.ValueObjects;
using EventsBookingBackend.Domain.Common.ValueObjects;
using EventsBookingBackend.Domain.Event.Repositories;
using EventsBookingBackend.Domain.Event.Specifications;
using EventsBookingBackend.Domain.File.Services;
using EventsBookingBackend.Domain.User.Repositories;
using EventsBookingBackend.Domain.User.Specifactions;
using EventsBookingBackend.Infrastructure.Payment.Payme.Services;
using Microsoft.AspNetCore.Identity;

namespace EventsBookingBackend.Application.Services.User;

public class UserService(
    IUserRepository userRepository,
    ILikedEventRepository likedEventRepository,
    IAuthService authService,
    IFileDomainService fileDomainService,
    IHttpContextAccessor httpContextAccessor,
    UserManager<Domain.Auth.Entities.Auth> userManager,
    IBookingRepository bookingRepository,
    IPaymeGenerateUrlService paymeGenerateUrlService,
    IMapper mapper) : IUserService
{
    public async Task<GetUserProfileResponse> GetUserProfile()
    {
        var userId = (Guid)authService.GetCurrentAuthUserId()!;
        var auth = await authService.GetCurrentAuthUser();
        var profile = mapper.Map<GetUserProfileResponse>(
            await userRepository.FindFirst(new GetUserProfileSpecification(userId)));
        profile.Phone = auth!.PhoneNumber;
        return profile;
    }

    public async Task<List<GetUserBookedEventResponse>> GetUserBookedEvents()
    {
        var bookings =
            await bookingRepository.FindAll(new GetUserBookedEvents((Guid)authService.GetCurrentAuthUserId()!));
        var results = mapper.Map<List<GetUserBookedEventResponse>>(bookings);
        foreach (var result in results)
            if (result.Status == BookingStatus.Waiting)
                result.PaymentUrl = paymeGenerateUrlService.GenerateUrl(result.Id.ToString(), result.Cost * 100);
        return results;
    }

    public async Task<List<GetUserLikedEventResponse>> GetUserLikedEvents()
    {
        var likedEvents =
            await likedEventRepository.FindAll(new GetLikedEventByUser((Guid)authService.GetCurrentAuthUserId()!));
        return mapper.Map<List<GetUserLikedEventResponse>>(likedEvents.Select(e => e.Event));
    }

    public async Task ChangeProfile(ChangeProfileRequest request)
    {
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var currentUser =
            (await userRepository.FindFirst(
                new GetUserProfileSpecification(authService.GetCurrentAuthUserId()!.Value)))!;
        if (request.Name != null)
            currentUser.Name = request.Name;

        if (request.Password != null)
        {
            var auth = await userManager.GetUserAsync(httpContextAccessor.HttpContext!.User);
            if (request.Password != request.RepeatPassword)
            {
                throw new AppValidationException("Пароли не совпадают !");
            }

            var result = await userManager.ChangePasswordAsync(auth!, request.OldPassword!, request.Password);
            if (result.Succeeded == false)
            {
                throw new AppValidationException(result.Errors.First().Description);
            }
        }

        if (request.Phone != null)
        {
            var auth = await userManager.GetUserAsync(httpContextAccessor.HttpContext!.User);
            auth!.PhoneNumber = request.Phone;
            auth.UserName = request.Phone;
            await userManager.UpdateAsync(auth);
        }

        await userRepository.Update(currentUser);
        transactionScope.Complete();
    }

    public async Task<ChangeAvatarResponse> ChangeAvatar(IFormFile avatar)
    {
        string path = await fileDomainService.UploadFileAsync(avatar);
        var currentUser =
            (await userRepository.FindFirst(
                new GetUserProfileSpecification(authService.GetCurrentAuthUserId()!.Value)))!;
        currentUser.Avatar = new FileValueObject()
        {
            Path = path,
        };
        await userRepository.Update(currentUser);
        return new ChangeAvatarResponse()
        {
            Avatar = new FileDto()
            {
                Path = path
            }
        };
    }
}