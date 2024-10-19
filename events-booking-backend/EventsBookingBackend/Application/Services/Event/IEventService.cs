using EventsBookingBackend.Application.Models.Event.Requests;
using EventsBookingBackend.Application.Models.Event.Responses;

namespace EventsBookingBackend.Application.Services.Event;

public interface IEventService
{
    public Task<IList<GetAllEventsResponse>> GetAllEvents();
    public Task<GetEventDetailResponse> GetEventDetail(GetEventDetailRequest request);
    public Task<LikeEventResponse> LikeEvent(LikeEventRequest request);

}