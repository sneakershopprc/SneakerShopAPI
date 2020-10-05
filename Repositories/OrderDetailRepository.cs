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
    public class OrderDetailRepository : BaseRepository
    {
        private readonly IMapper mapper;
        private readonly ProductDetailRepository productDetailRepository;

        public OrderDetailRepository(SneakerShopContext context, IMapper mapper, ProductDetailRepository _productDetailRepository) : base(context)
        {
            this.mapper = mapper;
            productDetailRepository = _productDetailRepository;

        }
        public PagedList<OrderDetail> GetAll(SearchOrderVModel model)
        {
            var query = context.OrderDetail.Where(d => (model.OrderId == null || d.OrderId == model.OrderId))
                    .Select(s => new OrderDetail
                    {
                        Id = s.Id,
                        OrderId = s.OrderId,
                        Product = s.Product,
                        Quantity = s.Quantity,
                        Price = s.Price,
                        Discount = s.Discount
                    });

            var totalCount = query.Count();
            List<OrderDetail> result = null;
            result = query.Skip(model.Size * (model.Page - 1))
            .Take(model.Size)
            .ToList();
            return PagedList<OrderDetail>.ToPagedList(result, totalCount, model.Page, model.Size);
        }


        public bool Create(OrderDetail orderDetail)
        {
            context.OrderDetail.Add(orderDetail);
            return true;
        }


        public bool checkOrderDetail(List<OrderDetailVModel> orderDetails)
        {
            string errorMsg = "";
            foreach(OrderDetailVModel detailVModel in orderDetails)
            {
                ProductDetail productDetail = productDetailRepository.Get(detailVModel.DetailId);
                if(productDetail.Quantity < detailVModel.Quantity)
                {
                    return false;
                }
            }
            return true;
        }


    }
}
