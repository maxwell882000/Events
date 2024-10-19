using EventsBookingBackend.Infrastructure.Payment.Payme.Entities;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Dto;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Requests;
using EventsBookingBackend.Infrastructure.Payment.Payme.Models.Responses;
using EventsBookingBackend.Infrastructure.Payment.Payme.ValueObjects;

namespace EventsBookingBackend.Infrastructure.Payment.Payme.Profile;

public class PaymeProfile : AutoMapper.Profile
{
    public PaymeProfile()
    {
        CreateMap<CreateTransactionRequest, CheckPerformTransactionRequest>();
        CreateMap<CreateTransactionRequest, TransactionDetail<Account>>()
            .ForMember(dest => dest.PaymeId, opt => opt.MapFrom(src => src.Id));
        CreateMap<TransactionDetail<Account>, CreateTransactionResponse>()
            .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src.Id));
        CreateMap<TransactionDetail<Account>, PerformTransactionResponse>()
            .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src.Id));
        CreateMap<TransactionDetail<Account>, CancelTransactionResponse>()
            .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src.Id));
        CreateMap<TransactionDetail<Account>, CheckTransactionResponse>()
            .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src.Id));
        CreateMap<TransactionDetail<Account>, GetStatementResponse.TransactionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PaymeId))
            .ForMember(dest => dest.Transaction, opt => opt.MapFrom(src => src.Id));

        CreateMap<AccountDto, Account>();
    }
}