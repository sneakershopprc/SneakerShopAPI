using AutoMapper;
using SneakerShopAPI.Models;
using SneakerShopAPI.ViewModels;
using ssrcore.Helpers;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.Repositories
{
    public class WishListRepository : BaseRepository
    {
        private readonly IMapper mapper;
        private readonly ProductRepository productRepository;

        public WishListRepository(SneakerShopContext context, IMapper mapper, ProductRepository _productRepository) : base(context)
        {
            this.mapper = mapper;
            productRepository = _productRepository;

        }
        public WishList Get(string productId, string username)
        {
            WishList wishList = context.WishList.SingleOrDefault(s => s.Username == username && s.ProductId == productId && s.DelFlg == false);
            return wishList;
        }
        public WishListVModel GetToVModel(string productId, string username)
        {
            WishListVModel wishList = context.WishList.Where(s => s.Username == username && s.ProductId == productId && s.DelFlg == false)
                .Select(s => new WishListVModel
                {
                    Id = s.Id,
                    Product = new ProductVModel
                    {
                        ProductId = s.ProductId,
                        ProductNm = s.Product.ProductNm,
                        Description = s.Product.Description,
                        Color = s.Product.Color,
                        BrandId = s.Product.BrandId,
                        BrandNm = s.Product.Brand.BrandNm,
                        Price = s.Product.ProductDetail.Count > 0 ? s.Product.ProductDetail.Min(t => t.Price) : 0,
                        Discount = s.Product.Discount,
                        photoList = s.Product.PhotoProduct.Where(d => d.DelFlg == false).Select(p => p.Photo).ToList(),
                    }
                }).SingleOrDefault();
            return wishList;
        }
        public PagedList<WishListVModel> GetAll(ResourceParameters model, string username)
        {
            var query = context.WishList.Where(d => (d.DelFlg == false)
                        && (d.Username == username))
                    .Select(s => new WishListVModel
                    {
                        Id = s.Id,
                        Product = new ProductVModel
                        {
                            ProductId = s.ProductId,
                            ProductNm = s.Product.ProductNm,
                            Description = s.Product.Description,
                            Color = s.Product.Color,
                            BrandId = s.Product.BrandId,
                            BrandNm = s.Product.Brand.BrandNm,
                            Price = s.Product.ProductDetail.Count > 0 ? s.Product.ProductDetail.Min(t => t.Price) : 0,
                            Discount = s.Product.Discount,
                            photoList = s.Product.PhotoProduct.Where(d => d.DelFlg == false).Select(p => p.Photo).ToList(),
                        }
                    }
                    );
            var totalCount = query.Count();
            List<WishListVModel> result = null;
            result = query.Skip(model.Size * (model.Page - 1))
            .Take(model.Size)
            .ToList();
            return PagedList<WishListVModel>.ToPagedList(result, totalCount, model.Page, model.Size);
        }


        public bool Create(WishListVModel model)
        {
            var wishList = this.mapper.Map<WishList>(model);
            wishList.DelFlg = false;
            wishList.Username = model.Username;
            wishList.InsBy = model.Username;
            wishList.InsDatetime = DateTime.Now;
            wishList.UpdBy = model.Username;
            wishList.UpdDatetime = DateTime.Now;
            context.WishList.Add(wishList);
            context.SaveChanges();
            return true;
        }

        public bool Delete(string productId, string username)
        {
            WishList wishList = Get(productId, username);
            //wishList.DelFlg = true;
            //wishList.UpdBy = username;
            //wishList.UpdDatetime = DateTime.Now;
            context.WishList.Remove(wishList);
            context.SaveChanges();
            return true;
        }

    }
}
