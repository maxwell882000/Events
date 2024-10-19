using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Requests;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Responses;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Services;

public interface IPaymeService
{
    public Task<PaymeSuccessResponse> Pay(PaymeRequest payment);
}