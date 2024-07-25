using Application.Common.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Profiles;

public class AccountsProfile : Profile
{
    public AccountsProfile()
    {
        CreateMap<Account, AccountDTO>()
            .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name));

    }
}