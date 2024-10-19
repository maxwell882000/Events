using System.ComponentModel.DataAnnotations;
using EventsBookingBackend.Application.Models.Booking.Dto;

namespace EventsBookingBackend.Application.Models.Booking.Requests;

public class CreateBookingTypeRequest
{
    [Required] public string? Label { get; set; }
    [Required] public Guid? CategoryId { get; set; }
    [Required] public IList<BookingOptionDto>? BookingOptions { get; set; }
}