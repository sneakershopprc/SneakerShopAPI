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
        private readonly ProductRepository productRepository;

        public OrderDetailRepository(SneakerShopContext context, IMapper mapper, ProductRepository _productRepository) : base(context)
        {
            this.mapper = mapper;
            productRepository = _productRepository;

        }
        public List<OrderDetailVModel> GetAll(string orderId)
        {
            var result = context.OrderDetail.Where(d => (orderId == null || d.OrderId == orderId))
                    .Select(s => new OrderDetailVModel
                    {
                        Product = s.Product,
                        Quantity = (int)s.Quantity,
                        Price = s.Price,
                        Discount = s.Discount
                    }).ToList();
            return result;
        }


        public bool Create(OrderDetail orderDetail)
        {
            context.OrderDetail.Add(orderDetail);
            return true;
        }

      

    }
}
