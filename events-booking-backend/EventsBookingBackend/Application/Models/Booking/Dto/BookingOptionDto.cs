using System.ComponentModel.DataAnnotations;
using EventsBookingBackend.Domain.Booking.ValueObjects;

namespace EventsBookingBackend.Application.Models.Booking.Dto;

public class BookingOptionDto
{
    public Guid? Id { get; set; }
    public string? Label { get; set; }
    public BookingOptionType? Type { get; set; }
    public List<BookingOptionValueDto>? BookingOptionValues { get; set; }
}