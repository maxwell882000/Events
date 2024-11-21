using EventsBookingBackend.Domain.Booking.ValueObjects;

namespace EventsBookingBackend.Application.Models.User.Responses;

public class GetUserBookedEventResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Options { get; set; }
    public long Cost { get; set; }
    public BookingStatus Status { get; set; }
    public BookingGroupStatus GroupStatus { get; set; }
    public string? PaymentUrl { get; set; } = null;
}