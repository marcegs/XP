using Application.Common.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Profiles;

public class ProductProfile: Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDTO>()
            .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dto => dto.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
            .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id));
    }
}