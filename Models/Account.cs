using System;
using System.Collections.Generic;

namespace SneakerShopAPI.Models
{
    public partial class Account
    {
        public Account()
        {
            Order = new HashSet<Order>();
            ShippingAddress = new HashSet<ShippingAddress>();
            WishList = new HashSet<WishList>();
        }

        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string Role { get; set; }
        public bool? DelFlg { get; set; }
        public string InsBy { get; set; }
        public DateTime? InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime? UpdDatetime { get; set; }

        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<ShippingAddress> ShippingAddress { get; set; }
        public virtual ICollection<WishList> WishList { get; set; }
    }
}
