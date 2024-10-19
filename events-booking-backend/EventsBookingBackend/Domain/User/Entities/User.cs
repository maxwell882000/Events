using EventsBookingBackend.Domain.Common.Entities;
using EventsBookingBackend.Domain.Common.ValueObjects;

namespace EventsBookingBackend.Domain.User.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public FileValueObject? Avatar { get; set; }
}