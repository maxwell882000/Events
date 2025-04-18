using Coravel;
using EventsBookingBackend.DependencyInjections;
using EventsBookingBackend.Domain.Booking.Events;
using EventsBookingBackend.Infrastructure.Notification.Telegram.Extensions;
using EventsBookingJob.Consumers.Notification;
using EventsBookingJob.Job.Booking;
using EventsBookingJob.Job.Event;


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
        .EveryMinute()
        .PreventOverlapping(nameof(AggregateReviewJob));
    scheduler.Schedule<BookingEventJob>()
        .EveryThirtyMinutes()
        .PreventOverlapping(nameof(BookingEventJob));
});
app.Run();