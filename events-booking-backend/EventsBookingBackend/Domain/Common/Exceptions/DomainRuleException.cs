namespace EventsBookingBackend.Domain.Common.Exceptions;

public class DomainRuleException(string message) : Exception(message);