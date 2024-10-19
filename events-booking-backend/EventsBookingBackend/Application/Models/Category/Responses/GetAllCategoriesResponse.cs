using EventsBookingBackend.Application.Models.Common;

namespace EventsBookingBackend.Application.Models.Category.Responses;

public class GetAllCategoriesResponse
{
    public Guid Id { get; set; }
    public FileDto Icon { get; set; }
    public string BgColor { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; }
}