using EventsBookingBackend.Domain.Common.Specifications;
using EventsBookingBackend.Infrastructure.Payment.Payme.Entities;
using EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Specifications;

public class GetTransactionById(string id) : ISpecification<TransactionDetail<Account>>
{
    public IQueryable<TransactionDetail<Account>> Apply(IQueryable<TransactionDetail<Account>> query)
    {
        return query.Where(e => e.PaymeId == id);
    }
}