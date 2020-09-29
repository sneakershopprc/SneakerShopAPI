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
    public class WishListRepository : BaseRepository
    {
        private readonly IMapper mapper;
        private readonly ProductRepository productRepository;

        public WishListRepository(SneakerShopContext context, IMapper mapper, ProductRepository _productRepository) : base(context)
        {
            this.mapper = mapper;
            productRepository = _productRepository;

        }
        public WishList Get(int Id)
        {
            WishList wishList = context.WishList.SingleOrDefault(s => s.Id == Id && s.DelFlg == false);
            return wishList;
        }
        public PagedList<WishListVModel> GetAll(SearchWishListVModel model)
        {
            var query = context.WishList.Where(d => (d.DelFlg == false)
                        && (model.Username == null || d.Username == model.Username))
                    .Select(s => new WishListVModel
                    {
                        Id = s.Id,
                        ProductId = s.ProductId,
                        //Product = mapper.Map<ProductVModel>(s.Product),
                        Username = s.Username
                    }); 

            var totalCount = query.Count();
            List<WishListVModel> result = null;
            if (model.SortBy == Constants.SortBy.SORT_NAME_ASC)
            {
                query = query.OrderBy(t => t.Product.ProductNm);
            }
            else if (model.SortBy == Constants.SortBy.SORT_NAME_DES)
            {
                query = query.OrderByDescending(t => t.Product.ProductNm);
            }
            result = query.Skip(model.Size * (model.Page - 1))
            .Take(model.Size)
            .ToList();
            // set productvmodel
            for (int i = 0; i < result.Count; i++){
                result.ElementAt(i).Product = productRepository.GetToVModel(result.ElementAt(i).ProductId, 0);
            }
            return PagedList<WishListVModel>.ToPagedList(result, totalCount, model.Page, model.Size);
        }


        public bool Create(WishListVModel model)
        {
            var wishList = this.mapper.Map<WishList>(model);
            wishList.DelFlg = false;
            wishList.InsBy = model.Username;
            wishList.InsDatetime = DateTime.Now;
            wishList.UpdBy = model.Username;
            wishList.UpdDatetime = DateTime.Now;
            context.WishList.Add(wishList);
            context.SaveChanges();
            return true;
        }

        public bool Delete(int Id, string username)
        {
            WishList wishList = Get(Id);
            wishList.DelFlg = true;
            wishList.UpdBy = username;
            wishList.UpdDatetime = DateTime.Now;
            context.SaveChanges();
            return true;
        }

    }
}
