using AutoMapper;
using CatalogService.Dtos;
using CatalogService.Models;

namespace CatalogService.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductReadDto, ProductPublishDto>();
        }
    }
}