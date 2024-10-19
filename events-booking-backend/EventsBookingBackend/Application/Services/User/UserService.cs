using AutoMapper;
using EventsBookingBackend.Application.Models.User.Responses;
using EventsBookingBackend.Application.Services.Auth;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Booking.Specifications;
using EventsBookingBackend.Domain.Event.Repositories;
using EventsBookingBackend.Domain.Event.Specifications;
using EventsBookingBackend.Domain.User.Repositories;
using EventsBookingBackend.Domain.User.Specifactions;

namespace EventsBookingBackend.Application.Services.User;

public class UserService(
    IUserRepository userRepository,
    ILikedEventRepository likedEventRepository,
    IAuthService authService,
    IBookingRepository bookingRepository,
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
        var bookings = await bookingRepository.FindAll(new GetUserBookedEvents((Guid)authService.GetCurrentAuthUserId()!));
        return mapper.Map<List<GetUserBookedEventResponse>>(bookings);
    }

    public async Task<List<GetUserLikedEventResponse>> GetUserLikedEvents()
    {
        var likedEvents =
            await likedEventRepository.FindAll(new GetLikedEventByUser((Guid)authService.GetCurrentAuthUserId()!));
        return mapper.Map<List<GetUserLikedEventResponse>>(likedEvents.Select(e => e.Event));
    }
}