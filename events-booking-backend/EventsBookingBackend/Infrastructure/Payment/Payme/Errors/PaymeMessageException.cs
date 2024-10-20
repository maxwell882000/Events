using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Dto;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Errors;

public class PaymeMessageException : Exception
{
    public PaymeErrors Code { get; set; }
    public MessageDto? Message { get; set; }


    public static PaymeMessageException InvalidAmount()
    {
        return new PaymeMessageException()
        {
            Code = PaymeErrors.InvalidAmount,
            Message = new MessageDto()
            {
                En = "Invalid amount",
                Ru = "Недопустимая сумма",
                Uz = "Noto'g'ri miqdor"
            }
        };
    }

    public static PaymeMessageException TransactionNotFound()
    {
        return new PaymeMessageException()
        {
            Code = PaymeErrors.TransactionNotFound,
            Message = new MessageDto()
            {
                En = "Transaction not found",
                Ru = "Транзакция не найдена",
                Uz = "Tranzaksiya topilmadi"
            }
        };
    }

    public static PaymeMessageException InvalidOperation()
    {
        return new PaymeMessageException()
        {
            Code = PaymeErrors.InvalidOperation,
            Message = new MessageDto()
            {
                En = "Invalid operation",
                Ru = "Недопустимая операция",
                Uz = "Noto'g'ri operatsiya"
            }
        };
    }

    public static PaymeMessageException InvalidCancelOperation()
    {
        return new PaymeMessageException()
        {
            Code = PaymeErrors.InvalidCancelOperation,
            Message = new MessageDto()
            {
                En = "Invalid cancel operation",
                Ru = "Недопустимая операция отмены",
                Uz = "Noto'g'ri bekor qilish operatsiyasi"
            }
        };
    }


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