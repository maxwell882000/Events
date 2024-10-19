namespace EventsBookingBackend.Application.Models.Common;

public class ValidationErrorModel : ErrorModel
{
    public IDictionary<string, string[]>? Errors { get; set; }
}