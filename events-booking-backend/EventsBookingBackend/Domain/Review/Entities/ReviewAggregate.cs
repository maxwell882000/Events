namespace EventsBookingBackend.Domain.Review.Entities;

public class ReviewAggregate(double overallMark, int reviewCount)
{
    public double Mark => _mark;
    private double _mark = reviewCount == 0 ? 0 : overallMark / reviewCount;
    public int ReviewCount { get; private set; } = reviewCount;
}