using EventsBookingBackend.Domain.Common.Entities;
using EventsBookingBackend.Domain.Common.ValueObjects;

namespace EventsBookingBackend.Domain.Booking.Entities;

public class BookingType : BaseEntity
{
    public string Label { get; set; }
    public Guid CategoryId { get; set; }
    public decimal Cost { get; set; }
    public long CostInTiyn => (long)Cost * 100;
    public FileValueObject Icon { get; set; }
    public int Order { get; set; }
    public IList<BookingOption> BookingOptions { get; set; }
    public bool IsShowLimit { get; set; } = false;
}