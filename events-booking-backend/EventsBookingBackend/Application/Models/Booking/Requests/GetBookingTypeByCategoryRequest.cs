using System.ComponentModel.DataAnnotations;

namespace EventsBookingBackend.Application.Models.Booking.Requests;

public class GetBookingTypeByCategoryRequest
{
    [Required] public Guid CategoryId { get; set; }
}