using System.ComponentModel.DataAnnotations;

namespace EventsBookingBackend.Application.Models.Review.Requests;

public class UpdateReviewRequest
{
    [Required] [Range(1, 5)] public int? Mark { get; set; }

    [Required] public Guid? EventId { get; set; }
    public string? Comment { get; set; }
}