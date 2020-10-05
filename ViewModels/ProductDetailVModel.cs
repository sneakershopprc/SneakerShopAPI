using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.ViewModels
{
    public class ProductDetailVModel
    {
        public string ProductId { get; set; }
        public double Size { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string ProductNm { get; set; }
        public string Color { get; set; }
        public double Discount { get; set; }
        public string BrandNm { get; set; }

    }
}
