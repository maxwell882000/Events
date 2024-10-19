using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using EventsBookingTests.Static;
using Microsoft.EntityFrameworkCore;

namespace EventsBookingTests.Factory;

public class DbContextOptionsFactory<T> where T : DbContext
{
    public static  DbContextOptions<T>? Create()
    {
        var options = new DbContextOptionsBuilder<T>()
            .UseNpgsql(ConnectionString.Value)
            .UseSnakeCaseNamingConvention()
            .Options;
        return options;
    }
}