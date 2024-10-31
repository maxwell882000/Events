using System.ComponentModel.DataAnnotations;

namespace EventsBookingBackend.Application.Models.Event.Requests;

public class GetEventDetailRequest
{
    [Required] public Guid? Id { get; set; }
}