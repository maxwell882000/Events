namespace EventsBookingBackend.Application.Models.Review.Responses;

public class UpdateReviewResponse
{
    public Guid Id { get; set; }
    public double Mark { get; set; }
    public int ReviewCount { get; set; }
}