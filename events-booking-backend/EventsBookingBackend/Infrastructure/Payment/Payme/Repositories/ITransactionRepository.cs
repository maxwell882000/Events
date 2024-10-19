using EventsBookingBackend.Domain.Common.Repositories;
using EventsBookingBackend.Infrastructure.Payment.Payme.Entities;
using EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Repositories;

public interface ITransactionRepository : IBaseRepository<TransactionDetail<Account>>
{


}