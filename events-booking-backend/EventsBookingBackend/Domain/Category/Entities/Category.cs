using EventsBookingBackend.Domain.Common.Entities;
using EventsBookingBackend.Domain.Common.ValueObjects;

namespace EventsBookingBackend.Domain.Category.Entities;

public class Category : BaseEntity
{
    public FileValueObject Icon { get; set; }
    public string BgColor { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public bool IsDefault { get; set; }
}