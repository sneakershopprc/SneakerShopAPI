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
    public class BrandRepository : BaseRepository
    {
        private readonly IMapper mapper;

        public BrandRepository(SneakerShopContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;

        }
        public Brand Get(string brandId)
        {
            Brand brand = context.Brand.SingleOrDefault(s => s.BrandId == brandId && s.DelFlg == false);
            return brand;
        }
        public BrandVModel GetToVModel(string brandId)
        {
            var model = context.Brand.Where(s => s.DelFlg == false && s.BrandId == brandId).Select(s => new BrandVModel
            {
                BrandId = s.BrandId,
                BrandNm = s.BrandNm,
                Photo = s.Photo,
                Description = s.Description
            }).SingleOrDefault();
            return model;
        }

        public PagedList<BrandVModel> GetAll(SearchBrandVModel model)
        {
            var query = context.Brand.Where(d => (d.DelFlg == false)
                        && (model.BrandNm == null || d.BrandNm.Contains(model.BrandNm)))
                    .Select(s => new BrandVModel
                    {
                        BrandId = s.BrandId,
                        BrandNm = s.BrandNm,
                        Photo = s.Photo,
                        Description = s.Description
                    });

            var totalCount = query.Count();
            List<BrandVModel> result = null;
            if (model.SortBy == Constants.SortBy.SORT_NAME_ASC)
            {
                query = query.OrderBy(t => t.BrandNm);
            }
            else if (model.SortBy == Constants.SortBy.SORT_NAME_DES)
            {
                query = query.OrderByDescending(t => t.BrandNm);
            }
            result = query.Skip(model.Size * (model.Page - 1))
            .Take(model.Size)
            .ToList();
            return PagedList<BrandVModel>.ToPagedList(result, totalCount, model.Page, model.Size);
        }


        public BrandVModel Create(BrandVModel model)
        {
            var brand = this.mapper.Map<Brand>(model);
            brand.BrandId = GetId();
            brand.DelFlg = false;
            brand.InsBy = model.Implementer;
            brand.InsDatetime = DateTime.Now;
            brand.UpdBy = model.Implementer;
            brand.UpdDatetime = DateTime.Now;
            context.Brand.Add(brand);
            context.SaveChanges();
            return GetToVModel(brand.BrandId);
        }

        public bool Delete(string brandId, string implementer)
        {
            Brand brand = Get(brandId);
            brand.DelFlg = true;
            brand.UpdBy = implementer;
            brand.UpdDatetime = DateTime.Now;
            context.SaveChanges();
            return true;
        }

        public BrandVModel Update(string brandId, BrandVModel model)
        {
            Brand brand = Get(brandId);
            brand.BrandNm = model.BrandNm;
            brand.Photo = model.Photo;
            brand.Description = model.Description;
            brand.UpdBy = model.Implementer;
            brand.UpdDatetime = DateTime.Now;
            context.SaveChanges();
            return GetToVModel(brand.BrandId);
        }
        public string GetId()
        {
            IEnumerable<Brand> brandList = context.Brand.ToList();
            if (brandList.ToList().Count > 0)
            {
                int max = 0;
                foreach (var brand in brandList)
                {
                    string id = brand.BrandId;
                    int number = int.Parse(id.Substring(5));
                    if (max < number)
                    {
                        max = number;
                    }
                }
                max++;
                return "brand" + max;
            }
            return "brand1";
        }
    }
}
