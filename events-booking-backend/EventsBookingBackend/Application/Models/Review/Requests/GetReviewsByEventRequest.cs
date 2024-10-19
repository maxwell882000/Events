using System.ComponentModel.DataAnnotations;

namespace EventsBookingBackend.Application.Models.Review.Requests;

public class GetReviewsByEventRequest
{
    [Required]
    public Guid EventId { get; set; }
}