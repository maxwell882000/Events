using Coravel;
using EventsBookingBackend.DependencyInjections;
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