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
            // ShippingAddress Mapper
            CreateMap<ShippingAddress, ShippingAddressVModel>();
            CreateMap<ShippingAddressVModel, ShippingAddress>();
            // Brand Mapper
            CreateMap<Brand, BrandVModel>();
            CreateMap<BrandVModel, Brand>();
            // Product Mapper
            CreateMap<Product, ProductVModel>();
            CreateMap<ProductVModel, Product>();
            // WishList Mapper
            CreateMap<WishList, WishListVModel>();
            CreateMap<WishListVModel, WishList>();
        }
    }
}
