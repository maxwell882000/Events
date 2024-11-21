using EventsBookingBackend.Application.Models.Common;

namespace EventsBookingBackend.Application.Models.User.Responses;

public class ChangeAvatarResponse
{
    public FileDto Avatar { get; set; }
}