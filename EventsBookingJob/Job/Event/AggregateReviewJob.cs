using System;
using System.Threading.Tasks;
using Coravel.Invocable;
using EventsBookingBackend.Domain.Event.Entities;
using EventsBookingBackend.Domain.Event.Repositories;
using EventsBookingBackend.Domain.Event.Specifications;
using EventsBookingBackend.Domain.Review.Repositories;
using EventsBookingBackend.Domain.Review.Specifications;
using Microsoft.Extensions.Logging;

namespace EventsBookingJob.Job.Event;

public class AggregateReviewJob(
    IEventRepository eventRepository,
    IEventAggregatedReviewRepository eventAggregatedReviewRepository,
    IReviewAggregateRepository reviewAggregateRepository,
    ILogger<AggregateReviewJob> logger) : IInvocable
{
    public async Task Invoke()
    {
        logger.LogInformation("AggregateReviewJob Invocable");
        var allEvents = await eventRepository.FindAll();
        logger.LogInformation($"Loaded {allEvents.Count} events");
        var count = 0;
        var failed = 0;
        foreach (var eventEntity in allEvents)
        {
            try
            {
                var aggregatedReview =
                    await reviewAggregateRepository.GetReviewAggregate(new GetEventReviews(eventEntity.Id));


                var existingAggregatedReview =
                    await eventAggregatedReviewRepository.FindFirst(
                        new GetEventAggregatedReviewByEvent(eventEntity.Id));
                if (existingAggregatedReview != null)
                {
                    existingAggregatedReview.ReviewCount = aggregatedReview.ReviewCount;
                    existingAggregatedReview.OverallMark = aggregatedReview.Mark;
                    await eventAggregatedReviewRepository.Update(existingAggregatedReview);
                }
                else
                {
                    var eventAggregateReview = new EventAggregatedReview()
                    {
                        EventId = eventEntity.Id,
                        ReviewCount = aggregatedReview.ReviewCount,
                        OverallMark = aggregatedReview.Mark,
                    };
                    await eventAggregatedReviewRepository.Create(eventAggregateReview);
                }

                count++;
                logger.LogInformation(
                    $"Aggregate review updated for event {eventEntity.Id}. All {allEvents.Count}. Remained {allEvents.Count - count - failed}. Failed {failed}");
            }
            catch (Exception e)
            {
                failed++;
                logger.LogError(
                    $"Failed to update event review aggregate for event {eventEntity.Id}. Message : {e.Message}. Stack Trace: {e.StackTrace}");
            }
        }
    }
}