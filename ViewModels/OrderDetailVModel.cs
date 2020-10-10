using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.ViewModels
{
    public class OrderDetailVModel
    {
        public int ProductDetailId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }

    }
}
