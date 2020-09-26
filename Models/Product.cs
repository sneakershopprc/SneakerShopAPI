using System;
using System.Collections.Generic;

namespace SneakerShopAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            PhotoProduct = new HashSet<PhotoProduct>();
            WishList = new HashSet<WishList>();
        }

        public string ProductId { get; set; }
        public string ProductNm { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public decimal Size { get; set; }
        public string Description { get; set; }
        public double Discount { get; set; }
        public string BrandId { get; set; }
        public bool DelFlg { get; set; }
        public string InsBy { get; set; }
        public DateTime InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime UpdDatetime { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<PhotoProduct> PhotoProduct { get; set; }
        public virtual ICollection<WishList> WishList { get; set; }
    }
}
