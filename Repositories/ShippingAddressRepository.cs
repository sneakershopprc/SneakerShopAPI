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
    public class ShippingAddressRepository : BaseRepository
    {
        private readonly IMapper mapper;

        public ShippingAddressRepository(SneakerShopContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;

        }
        public ShippingAddress Get(int Id)
        {
            ShippingAddress shippingAddress = context.ShippingAddress.SingleOrDefault(s => s.Id == Id && s.DelFlg == false);
            return shippingAddress;
        }
        public PagedList<ShippingAddressVModel> GetAll(SearchShippingAddressVModel model)
        {
            var query = context.ShippingAddress.Where(d => (d.DelFlg == false)
                        && (model.Username == null || d.Username == model.Username))
                    .Select(s => new ShippingAddressVModel
                    {
                        Id = s.Id,
                        Address = s.Address,
                        Phonenumber = s.Phonenumber,
                        IsDefault = s.IsDefault,
                        Username = s.Username,
                        Fullname = s.UsernameNavigation.Fullname
                    });

            var totalCount = query.Count();
            List<ShippingAddressVModel> result = null;
            if (model.SortBy == Constants.SortBy.SORT_NAME_ASC)
            {
                query = query.OrderBy(t => t.Address);
            }
            else if (model.SortBy == Constants.SortBy.SORT_NAME_DES)
            {
                query = query.OrderByDescending(t => t.Address);
            }
            result = query.Skip(model.Size * (model.Page - 1))
            .Take(model.Size)
            .ToList();
            return PagedList<ShippingAddressVModel>.ToPagedList(result, totalCount, model.Page, model.Size);
        }


        public bool Create(ShippingAddressVModel model)
        {
            var shippingAddress = this.mapper.Map<ShippingAddress>(model);
            shippingAddress.DelFlg = false;
            shippingAddress.InsBy = model.Username;
            shippingAddress.InsDatetime = DateTime.Now;
            shippingAddress.UpdBy = model.Username;
            shippingAddress.UpdDatetime = DateTime.Now;
            context.ShippingAddress.Add(shippingAddress);
            context.SaveChanges();
            return true;
        }

        public bool Delete(int Id, string username)
        {
            ShippingAddress shippingAddress = Get(Id);
            shippingAddress.DelFlg = true;
            shippingAddress.UpdBy = username;
            shippingAddress.UpdDatetime = DateTime.Now;
            context.SaveChanges();
            return true;
        }

        public bool Update(int Id, ShippingAddressVModel model)
        {
            ShippingAddress shippingAddress = Get(Id);
            shippingAddress.Address = model.Address;
            shippingAddress.Phonenumber = model.Phonenumber;
            shippingAddress.IsDefault = model.IsDefault;
            shippingAddress.UpdBy = model.Username;
            shippingAddress.UpdDatetime = DateTime.Now;
            context.SaveChanges();
            return true;
        }
    }
}
