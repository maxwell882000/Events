using System.ComponentModel.DataAnnotations;

namespace EventsBookingBackend.Application.Models.Event.Requests;

public class GetEventDetailRequest
{
    [Required] public Guid? Id { get; set; }
    public MyEnum Chec { get; set; }
    public enum MyEnum
    {
        CHECK,
        SECOND
    }
}