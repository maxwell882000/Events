namespace EventsBookingBackend.Application.Models.Review.Responses;

public class CreateReviewResponse
{
    public Guid Id { get; set; }
    public double Mark { get; set; }
    public int ReviewCount { get; set; }
}