using EventsBookingBackend.DependencyInjections;
using Seeds.Seed.Booking;
using Seeds.Seed.Category;
using Seeds.Seed.Common;
using Seeds.Seed.Event;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ISeed, BookingSeed>();
builder.Services.AddTransient<ISeed, CategorySeed>();
builder.Services.AddTransient<ISeed, EventSeed>();
builder.Services.AddDatabases(builder.Configuration);
builder.Services.AddRepositories();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seed = scope.ServiceProvider.GetServices<ISeed>();
    foreach (var seedItem in seed)
        await seedItem.SeedAsync();
}