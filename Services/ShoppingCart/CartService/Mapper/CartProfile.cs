using AutoMapper;
using CartService.Dtos;
using CartService.Models;

namespace CartService.Mapper
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<ProductPublishDto, CartItem>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}