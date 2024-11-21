using System.ComponentModel.DataAnnotations;
using EventsBookingBackend.Application.Models.Common;

namespace EventsBookingBackend.Application.Models.User.Requests;

public class ChangeProfileRequest
{
    [Application.Common.Validations.Phone]
    [Required]
    public string? Phone { get; set; }

    [Required] public string? Name { get; set; }
    public string? Password { get; set; }
    public string? RepeatPassword { get; set; }
    public string? OldPassword { get; set; }
}