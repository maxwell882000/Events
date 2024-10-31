namespace EventsBookingBackend.Domain.Common.Exceptions;

public class DomainRuleException(
    string message,
    int status = (int)DomainExceptionStatus.Default) : Exception(message)
{
    public int Status = status;

    public static DomainRuleException NotFound(string message)
    {
        return new DomainRuleException(message, (int)DomainExceptionStatus.NotFound);
    }
}