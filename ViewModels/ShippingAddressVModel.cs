using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.ViewModels
{
    public class ShippingAddressVModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Phonenumber { get; set; }
        public bool IsDefault { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
    }
}
