using System.ComponentModel.DataAnnotations;

namespace EventsBookingBackend.Application.Models.Event.Requests;

public class LikeEventRequest
{
    [Required]
    public Guid? EventId { get; set; }
}