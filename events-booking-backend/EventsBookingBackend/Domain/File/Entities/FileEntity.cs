using EventsBookingBackend.Domain.Common.Entities;

namespace EventsBookingBackend.Domain.File.Entities;

public class File : BaseEntity
{
    public string FilePath { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }
    public long FileSize { get; set; }
    public bool IsActive { get; set; } = false;
}