using AutoMapper;
using SneakerShopAPI.Models;
using SneakerShopAPI.ViewModels;
using ssrcore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.Repositories
{
    public class ProductRepository : BaseRepository
    {
        private readonly IMapper mapper;
        private readonly ProductDetailRepository productDetailRepository;
        private readonly PhotoProductRepository photoProductRepository;

        public ProductRepository(SneakerShopContext context, IMapper mapper,
            ProductDetailRepository productDetailRepository, PhotoProductRepository photoProductRepository) : base(context)
        {
            this.mapper = mapper;
            this.productDetailRepository = productDetailRepository;
            this.photoProductRepository = photoProductRepository;
        }
        public Product Get(string productId)
        {
            Product product = context.Product.SingleOrDefault(s => s.ProductId == productId && s.DelFlg == false);
            return product;
        }
        public ProductVModel GetToVModel(string productId, int isStill)
        {
            //List<string> photoList = photoProductRepository.GetAll(productId);
            //List<ProductDetail> productDetailList = productDetailRepository.GetAll(productId, isStill);
            var productModel = context.Product.Where(s => s.DelFlg == false && s.ProductId == productId).Select(s => new ProductVModel
            {
                ProductId = s.ProductId,
                ProductNm = s.ProductNm,
                Description = s.Description,
                Color = s.Color,
                BrandId = s.BrandId,
                BrandNm = s.Brand.BrandNm,
                Price = s.ProductDetail.Count > 0 ? s.ProductDetail.Min(t => t.Price) : 0,
                Discount = s.Discount,
                photoList = s.PhotoProduct.Where(d => d.DelFlg == false).Select(p => p.Photo).ToList(),
                productDetailList = isStill == 1 ? s.ProductDetail.Where(pd => pd.Quantity > 0).ToList() : s.ProductDetail.ToList(),
                InsDatetime = s.InsDatetime,
                UpdDatetime = s.UpdDatetime
            }).SingleOrDefault();
            return productModel;
        }

        public PagedList<ProductVModel> GetAll(SearchProductVModel model)
        {
            // get all don't need to get productDetailList
            var query = context.Product.Where(d => (d.DelFlg == false)
                        && (model.ProductNm == null || d.ProductNm.Contains(model.ProductNm))
                        && (model.Color == null || d.Color.Contains(model.Color))
                        && (model.BrandList == null || model.BrandList.Contains(d.BrandId))
                        )
                    .Select(s => new ProductVModel
                    {
                        ProductId = s.ProductId,
                        ProductNm = s.ProductNm,
                        Description = s.Description,
                        Color = s.Color,
                        BrandId = s.BrandId,
                        BrandNm = s.Brand.BrandNm,
                        Price = s.ProductDetail.Count > 0 ? s.ProductDetail.Min(t => t.Price) : 0,
                        Discount = s.Discount,
                        photoList = s.PhotoProduct.Where(d => d.DelFlg == false).Select(p => p.Photo).ToList(),
                        InsDatetime = s.InsDatetime,
                        UpdDatetime = s.UpdDatetime
                        //productDetailList = model.isStill == 1 ? s.ProductDetail.Where(pd => pd.Quantity > 0).ToList() : s.ProductDetail.ToList()
                    }); 

            var totalCount = query.Count();
            List<ProductVModel> result = null;
            if (model.SortBy == Constants.SortBy.SORT_DEFAULT)
            {
                query = query.OrderByDescending(t => t.UpdDatetime);
            }
            else  if (model.SortBy == Constants.SortBy.SORT_NAME_ASC)
            {
                query = query.OrderBy(t => t.Price);
            }
            else if (model.SortBy == Constants.SortBy.SORT_NAME_DES)
            {
                query = query.OrderByDescending(t => t.Price);
            }
            result = query.Skip(model.Size * (model.Page - 1))
            .Take(model.Size)
            .ToList();
            return PagedList<ProductVModel>.ToPagedList(result, totalCount, model.Page, model.Size);
        }


        public ProductVModel Create(ProductVModel model)
        {
            var product = this.mapper.Map<Product>(model);
            product.ProductId = GetId();
            product.DelFlg = false;
            product.InsBy = model.Implementer;
            product.InsDatetime = DateTime.Now;
            product.UpdBy = model.Implementer;
            product.UpdDatetime = DateTime.Now;
            context.Product.Add(product);
            foreach (string photo in model.photoList)
            {
                PhotoProduct photoProduct = new PhotoProduct
                {
                    ProductId = product.ProductId,
                    Photo = photo,
                    DelFlg = false,
                    InsBy = model.Implementer,
                    InsDatetime = DateTime.Now,
                    UpdBy = model.Implementer,
                    UpdDatetime = DateTime.Now
                };
                photoProductRepository.Create(photoProduct);
            }
            context.SaveChanges();
            return GetToVModel(product.ProductId, model.isStill);
        }

        public bool Delete(string productId, string implementer)
        {
            Product product = Get(productId);
            product.DelFlg = true;
            product.UpdBy = implementer;
            product.UpdDatetime = DateTime.Now;
            photoProductRepository.DeleteByProductId(productId);

            context.SaveChanges();
            return true;
        }

        public ProductVModel Update(string productId, ProductVModel model)
        {
            Product product = Get(productId);
            product.ProductNm = model.ProductNm;
            product.Color = model.Color;
            product.Discount = model.Discount;
            product.Description = model.Description;
            product.UpdBy = model.Implementer;
            product.UpdDatetime = DateTime.Now;
            // delete all old photo 
            photoProductRepository.DeleteByProductId(productId);
            // add new photo list
            foreach (string photo in model.photoList)
            {
                PhotoProduct photoProduct = new PhotoProduct
                {
                    ProductId = product.ProductId,
                    Photo = photo,
                    DelFlg = false,
                    InsBy = model.Implementer,
                    InsDatetime = DateTime.Now,
                    UpdBy = model.Implementer,
                    UpdDatetime = DateTime.Now
                };
                photoProductRepository.Create(photoProduct);
            }
            // end
            context.SaveChanges();
            return GetToVModel(product.ProductId, model.isStill);
        }
        public string GetId()
        {
            IEnumerable<Product> productList = context.Product.ToList();
            if (productList.ToList().Count > 0)
            {
                int max = 0;
                foreach (var product in productList)
                {
                    string id = product.ProductId;
                    int number = int.Parse(id.Substring(3));
                    if (max < number)
                    {
                        max = number;
                    }
                }
                max++;
                return "pro" + max;
            }
            return "pro1";
        }
    }
}
