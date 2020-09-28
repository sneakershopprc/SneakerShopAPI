using AutoMapper;
using SneakerShopAPI.Models;
using SneakerShopAPI.ViewModels;
using ssrcore.ViewModels;

namespace ssrcore.AutoMapper
{
    public class BrandMapper : Profile
    {
        public BrandMapper()
        {
            CreateMap<Brand, BrandVModel>();
            CreateMap<BrandVModel, Brand>();
        }
    }
}
