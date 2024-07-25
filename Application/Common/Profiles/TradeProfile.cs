using Application.Common.DTOs;
using AutoMapper;
using Domain.Common;
using Domain.Entities;

namespace Application.Common.Profiles;

public class TradeProfile : Profile
{
    public TradeProfile()
    {
        CreateMap<Trade, TradeDTO>()
            .ForMember(dto => dto.Account, opt => opt.MapFrom(trade => trade.Account))
            .ForMember(dto => dto.TradeAmmount, opt => opt.MapFrom(trade => trade.TradeAmmount))
            .ForMember(dto => dto.TradeType, opt => opt.MapFrom(trade => trade.TradeType == TradeType.Sell ? "Sell" : "Buy"))
            .ForMember(dto => dto.Id, opt => opt.MapFrom(trade => trade.Id));
    }
}