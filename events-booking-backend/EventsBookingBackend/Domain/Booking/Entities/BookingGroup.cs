using System.Security.Cryptography;
using System.Text;
using EventsBookingBackend.Domain.Booking.ValueObjects;
using EventsBookingBackend.Domain.Common.Entities;
using Newtonsoft.Json;

namespace EventsBookingBackend.Domain.Booking.Entities;

public class BookingGroup : BaseEntity
{
    public Guid EventId { get; set; }
    public Guid BookingTypeId { get; set; }
    public BookingGroupStatus Status { get; set; } = BookingGroupStatus.Filling;
    public string UserOptions { get; set; }
    public IList<Booking> Bookings { get; set; } = [];
    public BookingType BookingType { get; set; }
    public DateTime? EndDate { get; set; }

    public static BookingGroup FromBooking(Booking booking)
    {
        return new BookingGroup()
        {
            EventId = booking.EventId,
            BookingTypeId = booking.BookingTypeId,
            UserOptions = booking.HashOptions(),
        };
    }

    public void SetStatus(BookingLimit? bookingLimit)
    {
        if (bookingLimit?.IsSingle == true)
        {
            Status = BookingGroupStatus.NoStatus;
        }
        else if (Bookings.Count == bookingLimit?.MaxBookings)
        {
            Status = BookingGroupStatus.Filled;
        }
        else
        {
            Status = BookingGroupStatus.Filling;
        }
    }

    public void CheckStarted()
    {
        if (Bookings.All(b => b.Status == BookingStatus.Paid))
        {
            Status = BookingGroupStatus.Started;
            EndDate = DateTime.Now.AddDays(BookingType.DurationInDays);
        }
    }
}