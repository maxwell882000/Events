using EventsBookingBackend.Application.Models.Booking.Dto;
using EventsBookingBackend.Application.Models.Common;

namespace EventsBookingBackend.Application.Models.Booking.Responses;

public class GetBookingTypeByCategoryResponse
{
    public Guid Id { get; set; }
    public string Label { get; set; }
    public Guid CategoryId { get; set; }
    public FileDto Icon { get; set; }
    public decimal Cost { get; set; }
    public bool IsShowLimit { get; set; }
    public int Order { get; set; }
    public IList<BookingOptionDto> BookingOptions { get; set; }
}