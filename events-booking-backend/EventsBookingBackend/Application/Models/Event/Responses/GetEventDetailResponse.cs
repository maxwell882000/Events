using EventsBookingBackend.Application.Models.Common;
using EventsBookingBackend.Application.Models.Event.Dtos;
using EventsBookingBackend.Domain.Event.ValueObjects;

namespace EventsBookingBackend.Application.Models.Event.Responses;

public class GetEventDetailResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NextTime { get; set; }
    public DayOfWeek WorkDay { get; set; }
    public bool IsOpen { get; set; }
    public FileDto Image { get; set; }
    public string Address { get; set; }
    public LatLong Coordinates { get; set; }
    public float Mark { get; set; } = 0;
    public int ReviewCount { get; set; } = 0;
    public Guid CategoryId { get; set; }
    public bool IsLiked { get; set; } = false;
    public bool IsReservable { get; set; } = false;
    public List<FileDto> Images { get; set; } = new();

    public List<WorkHourDto> WorkHours { get; set; }

    public List<BookingDetail> BookingDetails { get; set; } = new();

    public class BookingDetail
    {
        public string Label { get; set; }
        public decimal Cost { get; set; }
        public FileDto Icon { get; set; }
    }
}