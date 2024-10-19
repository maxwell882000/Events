using EventsBookingBackend.Infrastructure.Payment.Payme.Repositories;
using EventsBookingBackend.Infrastructure.Payment.Payme.Services;


namespace EventsBookingBackend.Infrastructure.Payment.Payme.DI;

public static class PaymeDI
{
    public static void AddPayme(this IServiceCollection services)
    {
        services.AddTransient<IPaymeService, PaymeService>();
        services.AddTransient<ITransactionRepository, TransactionRepository>();
    }
}