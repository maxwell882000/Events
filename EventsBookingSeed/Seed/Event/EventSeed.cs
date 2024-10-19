using EventsBookingBackend.Domain.Event.Repositories;
using Seeds.Seed.Common;

namespace Seeds.Seed.Event;

public class EventSeed(IEventRepository eventRepository, ILogger<EventSeed> logger)
    : BaseSeed<List<EventsBookingBackend.Domain.Event.Entities.Event>>("seed_events.json")
{
    protected override async Task SeedAsync(List<EventsBookingBackend.Domain.Event.Entities.Event> model)
    {
        var isEventSeeded = await eventRepository.FindFirst();

        if (isEventSeeded != null)
        {
            logger.LogInformation("EventSeed was seeded already");
            return;
        }

        foreach (var item in model)
        {
            await eventRepository.Create(item);
        }
    }
}