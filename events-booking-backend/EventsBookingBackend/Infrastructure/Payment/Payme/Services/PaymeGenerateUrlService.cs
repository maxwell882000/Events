using System.Text;
using EventsBookingBackend.Infrastructure.Payment.Payme.Option;
using Microsoft.Extensions.Options;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Services;

public class PaymeGenerateUrlService(IOptions<PaymeOption> paymeOption) : IPaymeGenerateUrlService
{
    public string GenerateUrl(string bookingId, long amount)
    {
        var merchantId = paymeOption.Value.MerchantId;
        string parameters = $"m={merchantId};ac.booking_id={bookingId};a={amount}";
        string base64EncodedParameters = Convert.ToBase64String(Encoding.UTF8.GetBytes(parameters));

        return $"https://checkout.paycom.uz/{base64EncodedParameters}";
    }
}