using System.ComponentModel.DataAnnotations;
using EventsBookingBackend.Application.Models.Booking.Dto;

namespace EventsBookingBackend.Application.Models.Booking.Requests;

public class CreateBookingRequest
{
    [Required] public Guid? EventId { get; set; }
    [Required] public Guid? BookingTypeId { get; set; }
    [Required] public List<BookingUserOptionDto>? BookingOptions { get; set; }
    public PaymentOption? Payment { get; set; } = PaymentOption.PAYME;

    public enum PaymentOption
    {
        PAYME,
        CLICK
    }
}