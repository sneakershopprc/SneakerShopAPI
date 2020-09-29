using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.ViewModels
{
    public class ProductDetailVModel
    {
        public string ProductId { get; set; }
        public decimal Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductNm { get; set; }
        public string Color { get; set; }
        public double Discount { get; set; }
        public string BrandNm { get; set; }

    }
}
