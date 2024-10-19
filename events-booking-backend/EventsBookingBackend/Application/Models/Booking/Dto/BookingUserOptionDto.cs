using System.ComponentModel.DataAnnotations;
using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.ValueObjects;

namespace EventsBookingBackend.Application.Models.Booking.Dto;

public class BookingUserOptionDto
{
    [Required]
    public Guid? OptionId { get; set; }
    [Required]
    public BookingOptionValue? BookingOptionValue { get; set; } // for DROP DOWN
}