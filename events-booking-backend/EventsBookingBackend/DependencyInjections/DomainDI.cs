
using EventsBookingBackend.Domain.Booking.Services;

namespace EventsBookingBackend.DependencyInjections;

public static class DomainDI
{
    public static void AddDomainServices(this IServiceCollection services)
    {

        services.AddTransient<IBookingDomainService, BookDomainService>();
    }
}