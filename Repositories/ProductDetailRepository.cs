using SneakerShopAPI.Models;
using SneakerShopAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.Repositories
{
    public class ProductDetailRepository : BaseRepository
    {

        public ProductDetailRepository(SneakerShopContext context) : base(context)
        {

        }
        public ProductDetail Get(string productId, double size)
        {
            ProductDetail productDetail = context.ProductDetail.SingleOrDefault(s => s.ProductId == productId && s.Size == size);
            return productDetail;
        }
        public ProductDetailVModel GetVModel(string productId, double size)
        {
            ProductDetailVModel productDetail = context.ProductDetail.Where(s => s.ProductId == productId && s.Size == size).Select(s => new ProductDetailVModel
            {
                ProductId = s.ProductId,
                Size = s.Size,
                Quantity = s.Quantity,
                Price = s.Price
            }).SingleOrDefault();
            return productDetail;
        }
   

        public List<ProductDetailVModel> GetAll(string productId)
        {
            //var productDetails = isStill == 1 ? context.ProductDetail.Where(d => d.ProductId == productId && d.Quantity > 0).ToList()
            //: context.ProductDetail.Where(d => d.ProductId == productId).ToList();
            var productDetails = context.ProductDetail.Where(d => d.ProductId == productId).Select(s => new ProductDetailVModel
            {
                ProductId = s.ProductId,
                Size = s.Size,
                Quantity = s.Quantity,
                Price = s.Price
            }).ToList();
            return productDetails;
        }


        public ProductDetailVModel Create(ProductDetail productDetail)
        {
            context.ProductDetail.Add(productDetail);
            context.SaveChanges();
            return GetVModel(productDetail.ProductId, productDetail.Size);
        }

        public ProductDetailVModel Update(ProductDetail model)
        {
            ProductDetail productDetail = Get(model.ProductId, model.Size);
            productDetail.Price = model.Price;
            productDetail.Quantity = model.Quantity;
            context.SaveChanges();
            return GetVModel(productDetail.ProductId, productDetail.Size);
        }
    }
}
