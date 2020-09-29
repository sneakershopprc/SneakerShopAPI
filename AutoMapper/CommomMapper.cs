using AutoMapper;
using SneakerShopAPI.Models;
using SneakerShopAPI.ViewModels;
using ssrcore.ViewModels;

namespace ssrcore.AutoMapper
{
    public class CommomMapper : Profile
    {
        public CommomMapper()
        {
            // Brand Mapper
            CreateMap<Brand, BrandVModel>();
            CreateMap<BrandVModel, Brand>();
            // Product Mapper
            CreateMap<Product, ProductVModel>();
            CreateMap<ProductVModel, Product>();
        }
    }
}
