using System;
using System.Collections.Generic;

namespace SneakerShopAPI.Models
{
    public partial class ShippingAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Phonenumber { get; set; }
        public bool IsDefault { get; set; }
        public string Username { get; set; }
        public bool DelFlg { get; set; }
        public string InsBy { get; set; }
        public DateTime InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime UpdDatetime { get; set; }

        public virtual Users UsernameNavigation { get; set; }
    }
}
