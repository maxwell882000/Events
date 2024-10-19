using System.ComponentModel.DataAnnotations;

namespace EventsBookingBackend.Application.Models.Auth.Requests;

public class AuthLoginRequest
{
    [Required]
    [Application.Common.Validations.Phone]
    public string? Phone { get; set; }

    [Required]
    public string? Password { get; set; }
}