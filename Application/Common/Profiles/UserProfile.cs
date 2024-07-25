using Application.Common.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>()
            .ForMember(dto => dto.Id,  opt => opt.MapFrom(a => a.Id))
            .ForMember(dto => dto.Accounts, opt => opt.MapFrom(a => a.Accounts))
            .ForMember(dto => dto.Birthdate, opt => opt.MapFrom(a => a.Birthdate))
            .ForMember(dto => dto.Username, opt => opt.MapFrom(a => a.Username));
    }
}