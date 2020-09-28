using System;
using System.Collections.Generic;

namespace SneakerShopAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public string OrderId { get; set; }
        public string Username { get; set; }
        public string ShippingAddress { get; set; }
        public string ReceiverNm { get; set; }
        public string Phonenumber { get; set; }
        public string Status { get; set; }
        public string InsBy { get; set; }
        public DateTime InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime UpdDatetime { get; set; }

        public virtual Account UsernameNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
