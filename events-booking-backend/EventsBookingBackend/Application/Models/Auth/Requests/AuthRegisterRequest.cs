using System.ComponentModel.DataAnnotations;

namespace EventsBookingBackend.Application.Models.Auth.Requests;

public class AuthRegisterRequest
{
    [Required]
    [Application.Common.Validations.Phone]
    public string? Phone { get; set; }

    [Required] public string? Name { get; set; }

    [Required] public string? Password { get; set; }

    [Required] public string? RepeatPassword { get; set; }
}