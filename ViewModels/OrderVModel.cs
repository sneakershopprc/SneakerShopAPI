using SneakerShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.ViewModels
{
    public class OrderVModel
    {
        public string OrderId { get; set; }
        public string Username { get; set; }
        public string ReceiverNm { get; set; }
        public string Phonenumber { get; set; }
        public string ShippingAddress { get; set; }
        public string Status { get; set; }
        public DateTime InsDatetime { get; set; }
        public DateTime UpdDatetime { get; set; }
        public List<OrderDetailVModel> OrderDetails { get; set; }
    }
}
