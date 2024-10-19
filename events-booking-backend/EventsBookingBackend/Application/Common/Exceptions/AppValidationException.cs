namespace EventsBookingBackend.Application.Common.Exceptions;

public class AppValidationException(string? message, Dictionary<string, string>? errors = null) : Exception(message)
{
    public Dictionary<string, string>? Errors { get; } = errors;
}