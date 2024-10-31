using System.Security.Cryptography;
using System.Text;
using EventsBookingBackend.Domain.Booking.ValueObjects;
using EventsBookingBackend.Domain.Common.Entities;
using Newtonsoft.Json;

namespace EventsBookingBackend.Domain.Booking.Entities;

public class Booking : BaseEntity
{
    public BookingStatus Status { get; set; } = BookingStatus.Waiting;
    public Guid EventId { get; set; }
    public Guid UserId { get; set; }
    public Guid BookingTypeId { get; set; }
    public Guid? BookingGroupId { get; set; }
    public List<BookingUserOption> BookingOptions { get; set; }
    public BookingType BookingType { get; set; }
    public BookingEvent Event { get; set; }
    public BookingGroup? BookingGroup { get; set; }

    public bool IsSameBooking(List<BookingUserOption> bookingUserOptions)
    {
        return bookingUserOptions.Count == BookingOptions.Count
               && BookingOptions.All(
                   e =>
                   {
                       var result = bookingUserOptions.Any(op =>
                       {
                           var res = op.OptionId == e.OptionId &&
                                     op.BookingOptionValue.Value == e.BookingOptionValue.Value;
                           return res;
                       });
                       return result;
                   }
               );
    }

    public string HashOptions()
    {
        // Serialize BookingOptions to JSON
        var json = JsonConvert.SerializeObject(BookingOptions.Select(e =>
            new { e.BookingOptionValue.Value, e.OptionId }));

        // Convert the JSON string to a byte array
        var bytes = Encoding.UTF8.GetBytes(json);

        // Compute the SHA256 hash
        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(bytes);

            // Convert the hash to a string (optional)
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }

    public void CancelBooking()
    {
        Status = BookingStatus.Canceled;
        BookingGroupId = null;
    }

    public bool IsWaitingPayment()
    {
        return Status is BookingStatus.Waiting or BookingStatus.PreparingToPay;
    }

    public void PaidBooking()
    {
        Status = BookingStatus.Paid;
    }
}