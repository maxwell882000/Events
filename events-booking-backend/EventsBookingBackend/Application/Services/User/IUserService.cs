using EventsBookingBackend.Application.Models.User.Requests;
using EventsBookingBackend.Application.Models.User.Responses;

namespace EventsBookingBackend.Application.Services.User;

public interface IUserService
{
    public Task<GetUserProfileResponse> GetUserProfile();
    public Task<List<GetUserBookedEventResponse>> GetUserBookedEvents();
    public Task<List<GetUserLikedEventResponse>> GetUserLikedEvents();
    public Task ChangeProfile(ChangeProfileRequest request);
    public Task<ChangeAvatarResponse> ChangeAvatar(IFormFile avatar);
}