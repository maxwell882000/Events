using System.ComponentModel.DataAnnotations;

namespace EventsBookingBackend.Application.Models.Booking.Dto;

public class BookingOptionValueDto
{
    [Required]
    public string? Value { get; set; }

}