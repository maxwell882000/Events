using AutoMapper;
using EventsBookingBackend.Application.Common.Exceptions;
using EventsBookingBackend.Application.Models.Event.Requests;
using EventsBookingBackend.Application.Models.Event.Responses;
using EventsBookingBackend.Application.Services.Auth;
using EventsBookingBackend.Application.Services.Book;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Booking.Specifications;
using EventsBookingBackend.Domain.Event.Entities;
using EventsBookingBackend.Domain.Event.Repositories;
using EventsBookingBackend.Domain.Event.Specifications;

namespace EventsBookingBackend.Application.Services.Event;

public class EventService(
    IEventRepository eventRepository,
    ILikedEventRepository likedEventRepository,
    IBookingTypeRepository bookingTypeRepository,
    IMapper mapper,
    IAuthService authService)
    : IEventService
{
    public async Task<IList<GetAllEventsResponse>> GetAllEvents()
    {
        var events =
            await eventRepository.FindAll(new GetAllEventsSpecification(authService.GetCurrentAuthUserId()));
        return mapper.Map<List<GetAllEventsResponse>>(events);
    }

    public async Task<GetEventDetailResponse> GetEventDetail(GetEventDetailRequest request)
    {
        var eventEntity =
            await eventRepository.FindFirst(
                new GetEventByIdSpecification((Guid)request.Id!, authService.GetCurrentAuthUserId()));
        if (eventEntity == null)
            throw new AppValidationException("Cобытие не найдено !");
        var response = mapper.Map<GetEventDetailResponse>(eventEntity);
        var bookingTypes = await bookingTypeRepository.FindAll(new GetBookingTypeByCategory(eventEntity.CategoryId));
        response.BookingDetails = mapper.Map<List<GetEventDetailResponse.BookingDetail>>(bookingTypes);
        return response;
    }

    public async Task<LikeEventResponse> LikeEvent(LikeEventRequest request)
    {
        return new LikeEventResponse()
        {
            IsLiked = await likedEventRepository.Upsert(new LikedEvent()
            {
                UserId = (Guid)authService.GetCurrentAuthUserId()!,
                EventId = (Guid)request.EventId!
            })
        };
    }
}