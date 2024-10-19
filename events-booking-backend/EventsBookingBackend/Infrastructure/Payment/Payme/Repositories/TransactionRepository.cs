using EventsBookingBackend.Infrastructure.Payment.Payme.Entities;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;
using EventsBookingBackend.Infrastructure.Repositories.Common;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Repositories;

public class TransactionRepository(PaymeDbContext paymeDbContext)
    : BaseRepository<TransactionDetail<ValueObjects.Account>, PaymeDbContext>(paymeDbContext), ITransactionRepository;