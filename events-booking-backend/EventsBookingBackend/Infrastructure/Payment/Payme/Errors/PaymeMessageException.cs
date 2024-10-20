using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Dto;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Errors;

public class PaymeMessageException : Exception
{
    public PaymeErrors Code { get; set; }
    public MessageDto? Message { get; set; }


    public static PaymeMessageException InvalidBookingId()
    {
        return new PaymeMessageException()
        {
            Code = PaymeErrors.InvalidBookingId, Message = new MessageDto()
            {
                En = "Invalid booking ID",
                Ru = "Неправильный идентификатор бронирования",
                Uz = "Noto'g'ri buyurtma identifikatori"
            }
        };
    }

    public static PaymeMessageException InvalidBookingStatus()
    {
        return new PaymeMessageException()
        {
            Code = PaymeErrors.InvalidBookingStatus, Message = new MessageDto()
            {
                En = "Invalid booking status",
                Ru = "Неправильный статус бронирования",
                Uz = "Noto'g'ri buyurtma holati"
            }
        };
    }
}