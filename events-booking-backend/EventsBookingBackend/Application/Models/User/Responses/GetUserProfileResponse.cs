using EventsBookingBackend.Application.Models.Common;

namespace EventsBookingBackend.Application.Models.User.Responses;

public class GetUserProfileResponse
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public FileDto? Avatar { get; set; }
}