using AutoMapper;
using Newtonsoft.Json;
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
    public class OrderRepository : BaseRepository
    {
        private readonly IMapper mapper;
        private readonly ProductRepository productRepository;
        private readonly ProductDetailRepository productDetailRepository;
        private readonly OrderDetailRepository orderDetailRepository;

        public OrderRepository(SneakerShopContext context, IMapper mapper, ProductRepository _productRepository, OrderDetailRepository _orderDetailRepository,
          ProductDetailRepository _productDetailRepository) : base(context)
        {
            this.mapper = mapper;
            productRepository = _productRepository;
            productDetailRepository = _productDetailRepository;
            orderDetailRepository = _orderDetailRepository;
        }
        public Order Get(string orderId)
        {
            Order order = context.Order.SingleOrDefault(s => s.OrderId == orderId);
            return order;
        }

        public OrderVModel GetToVModel(string orderId)
        {
            OrderVModel order = context.Order.Where(s => s.OrderId == orderId).Select(s => new OrderVModel
            {
                OrderId = s.OrderId,
                ReceiverNm = s.ReceiverNm,
                Phonenumber = s.Phonenumber,
                ShippingAddress = s.ShippingAddress,
                Status = s.Status,
                InsDatetime = (DateTime)s.InsDatetime,
                UpdDatetime = (DateTime)s.UpdDatetime,
                Username = s.Username
            }).SingleOrDefault();
            order.OrderDetails = orderDetailRepository.GetAll(orderId);
            return order;
        }
        public PagedList<OrderVModel> GetAll(SearchOrderVModel model)
        {
            var query = context.Order.Where(d => (model.Username == null || d.Username.Equals(model.Username)))
                    .Select(s => new OrderVModel
                    {
                        OrderId = s.OrderId,
                        ReceiverNm = s.ReceiverNm,
                        Phonenumber = s.Phonenumber,
                        ShippingAddress = s.ShippingAddress,
                        Status = s.Status,
                        InsDatetime = (DateTime)s.InsDatetime,
                        UpdDatetime = (DateTime)s.UpdDatetime,
                        Username = s.Username
                    });

            var totalCount = query.Count();
            List<OrderVModel> result = null;
            if (model.SortBy == Constants.SortBy.SORT_DEFAULT)
            {
                query = query.OrderByDescending(t => t.UpdDatetime);
            }
            result = query.Skip(model.Size * (model.Page - 1))
            .Take(model.Size)
            .ToList();
            foreach (OrderVModel orderVModel in result)
            {
                orderVModel.OrderDetails = orderDetailRepository.GetAll(orderVModel.OrderId);
            }
            return PagedList<OrderVModel>.ToPagedList(result, totalCount, model.Page, model.Size);
        }


        public OrderVModel Create(OrderVModel model)
        {
            var order = this.mapper.Map<Order>(model);
            order.OrderId = GetId();
            order.Status = Constants.Status.STATUS_WAITING;
            order.Username = model.Username;
            order.InsBy = model.Username;
            order.InsDatetime = DateTime.Now;
            order.UpdBy = model.Username;
            order.UpdDatetime = DateTime.Now;
            context.Order.Add(order);
            foreach (OrderDetailVModel orderDetailVModel in model.OrderDetails)
            {
                // get product detail in DB
                ProductDetailVModel productDetail = context.ProductDetail.Where(s => s.Id == orderDetailVModel.ProductDetailId)
                    .Select(s => new ProductDetailVModel
                    {
                        Id = s.Id,
                        ProductId = s.ProductId,
                        ProductNm = s.Product.ProductNm,
                        Color = s.Product.Color,
                        Size = s.Size,
                        Quantity = s.Quantity,
                        Price = s.Price,
                        Discount = s.Product.Discount,
                        BrandNm = s.Product.Brand.BrandNm,
                        Photo = s.Product.PhotoProduct.ElementAt(0).Photo
                    }).SingleOrDefault();
                // check quantity
                if (productDetail.Quantity < orderDetailVModel.Quantity)
                {
                    // not enough
                    return null;
                }
                // cast Vmodel to Model (had quantity)
                var orderDetail = mapper.Map<OrderDetail>(orderDetailVModel);
                orderDetail.OrderId = GetId();
                orderDetail.Product = JsonConvert.SerializeObject(productDetail);
                // add in OrderDetail table
                orderDetailRepository.Create(orderDetail);

                // minus quantity in product detail
                var productDetailUpdated = mapper.Map<ProductDetail>(productDetail);
                productDetailUpdated.Quantity -= orderDetailVModel.Quantity;
                productDetailRepository.Update(productDetailUpdated);
            }
            context.SaveChanges();
            return GetToVModel(order.OrderId);
        }

        public OrderVModel Update(string orderId, OrderVModel orderVModel)
        {
            Order order = Get(orderId);
            order.Status = orderVModel.Status;
            order.UpdBy = orderVModel.Username;
            order.UpdDatetime = DateTime.Now;

            // gọi cái order
            var orderDetails = orderDetailRepository.GetAll(orderId);
            if (orderVModel.Status.Equals(Constants.Status.STATUS_CANCEL))
            {
                foreach (OrderDetailVModel orderDetailVModel in orderDetails)
                {
                    ProductDetailVModel objFromJson = JsonConvert.DeserializeObject<ProductDetailVModel>(orderDetailVModel.Product);
                    // get product detail in DB
                    ProductDetail productDetail = context.ProductDetail.SingleOrDefault(s => s.Id == objFromJson.Id);
                    // add quantity in product detail
                    productDetail.Quantity += (int)orderDetailVModel.Quantity;
                    productDetailRepository.Update(productDetail);
                }
            }
            context.SaveChanges();
            return GetToVModel(order.OrderId);
        }
        public string GetId()
        {
            IEnumerable<Order> orderList = context.Order.ToList();
            if (orderList.ToList().Count > 0)
            {
                int max = 0;
                foreach (var order in orderList)
                {
                    string id = order.OrderId;
                    int number = int.Parse(id.Substring(5));
                    if (max < number)
                    {
                        max = number;
                    }
                }
                max++;
                return "order" + max;
            }
            return "order1";
        }
    }
}
