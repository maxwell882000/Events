using System;
using System.Threading.Tasks;
using Coravel.Invocable;
using EventsBookingBackend.Domain.Booking.Entities;
using EventsBookingBackend.Domain.Booking.Repositories;
using EventsBookingBackend.Domain.Booking.Specifications;
using EventsBookingBackend.Domain.Event.Repositories;
using Microsoft.Extensions.Logging;

namespace EventsBookingJob.Job.Booking;

public class BookingEventJob(
    IBookingEventRepository bookingEventRepository,
    IEventRepository eventRepository,
    ILogger<BookingEventJob> logger)
    : IInvocable
{
    public async Task Invoke()
    {
        logger.LogInformation("BookingEventJob Invocable");
        var allEvents = await eventRepository.FindAll();
        logger.LogInformation($"Loaded {allEvents.Count} events");
        var count = 0;
        var failed = 0;
        foreach (var eventEntity in allEvents)
        {
            try
            {
                var existingBookingEvent =
                    await bookingEventRepository.FindFirst(new GetBookingEventById(eventEntity.Id));
                if (existingBookingEvent == null)
                {
                    await bookingEventRepository.Create(new BookingEvent()
                    {
                        Id = eventEntity.Id,
                        Name = eventEntity.Name
                    });
                }
                else if (existingBookingEvent.UpdatedAt < eventEntity.UpdatedAt)
                {
                    existingBookingEvent.Name = eventEntity.Name;
                    await bookingEventRepository.Update(existingBookingEvent);
                }

                count++;
                logger.LogInformation(
                    $"BookingEventJob for event {eventEntity.Id}. All {allEvents.Count}. Remained {allEvents.Count - count - failed}. Failed {failed}");
            }
            catch (Exception e)
            {
                failed++;
                logger.LogError(
                    $"Failed to create booking event for event {eventEntity.Id}. Message : {e.Message}. Stack Trace: {e.StackTrace}");
            }
        }
    }
}