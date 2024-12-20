using Coravel;
using EventsBookingBackend.DependencyInjections;
using EventsBookingBackend.Domain.Booking.Events;
using EventsBookingBackend.Infrastructure.Notification.Telegram.Extensions;
using EventsBookingJob.Consumers.Notification;
using EventsBookingJob.Job.Booking;
using EventsBookingJob.Job.Event;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories();
builder.Services.AddDatabases(builder.Configuration);
builder.Services.AddScheduler();
builder.Services.AddTransient<AggregateReviewJob>();
builder.Services.AddTransient<BookingEventJob>();
builder.Services.AddTelegram(builder.Configuration);
builder.Services.AddEventBus(builder.Configuration, (x) =>
{
    x.AddConsumer<CreateBookingNotificationConsumer>();
    return x;
});

var app = builder.Build();

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<AggregateReviewJob>()
        .EveryFifteenSeconds()
        .PreventOverlapping(nameof(AggregateReviewJob));
    scheduler.Schedule<BookingEventJob>()
        .EveryFifteenSeconds()
        .PreventOverlapping(nameof(BookingEventJob));
});
app.Run();