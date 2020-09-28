using SneakerShopAPI.Models;
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

        public ProductDetail Get(string productId, decimal size)
        {
            ProductDetail productDetail = context.ProductDetail.SingleOrDefault(s => s.ProductId == productId && s.Size == size);
            return productDetail;
        }


        public List<ProductDetail> GetAll(string productId, int isStill)
        {
            var productDetails = isStill == 1 ? context.ProductDetail.Where(d => d.ProductId == productId && d.Quantity > 0).ToList()
            : context.ProductDetail.Where(d => d.ProductId == productId).ToList();
            return productDetails;
        }


        public ProductDetail Create(ProductDetail productDetail)
        {
            context.ProductDetail.Add(productDetail);
            context.SaveChanges();
            return Get(productDetail.ProductId, productDetail.Size);
        }

        public ProductDetail Update(ProductDetail model)
        {
            ProductDetail productDetail = Get(model.ProductId, model.Size);
            productDetail.Price = model.Price;
            productDetail.Quantity = model.Quantity;
            context.SaveChanges();
            return productDetail;
        }
    }
}
