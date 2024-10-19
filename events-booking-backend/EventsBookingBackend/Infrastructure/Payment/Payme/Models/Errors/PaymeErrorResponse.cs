namespace EventsBookingBackend.Infrastructure.Payment.Payme.Models.Errors;

public class PaymeErrorResponse : Exception
{
    public PaymeErrors Code { get; set; }
    public MessageDto? Message { get; set; }

    public class MessageDto
    {
        public string Ru { get; set; }
        public string En { get; set; }
        public string Uz { get; set; }
    }

    public static PaymeErrorResponse InvalidBookingId()
    {
        return new PaymeErrorResponse()
        {
            Code = PaymeErrors.InvalidBookingId, Message = new PaymeErrorResponse.MessageDto()
            {
                En = "Invalid booking ID",
                Ru = "Неправильный идентификатор бронирования",
                Uz = "Noto'g'ri buyurtma identifikatori"
            }
        };
    }

    public static PaymeErrorResponse InvalidBookingStatus()
    {
        return new PaymeErrorResponse()
        {
            Code = PaymeErrors.InvalidBookingStatus, Message = new PaymeErrorResponse.MessageDto()
            {
                En = "Invalid booking status",
                Ru = "Неправильный статус бронирования",
                Uz = "Noto'g'ri buyurtma holati"
            }
        };
    }
}