namespace EventsBookingBackend.Application.Models.Common;

public class ErrorModel
{
    public string Message { get; set; }
    public string? StackTrace { get; set; }
    public string Path { get; set; }
    public IDictionary<string, string>? Errors { get; set; }
}