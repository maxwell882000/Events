namespace EventsBookingBackend.Application.Models.User.Responses;

public class GetUserBookedEventResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Options { get; set; }
    public decimal Cost { get; set; }
}