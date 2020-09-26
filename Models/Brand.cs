using System;
using System.Collections.Generic;

namespace SneakerShopAPI.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Product = new HashSet<Product>();
        }

        public string BrandId { get; set; }
        public string BrandNm { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public bool DelFlg { get; set; }
        public string InsBy { get; set; }
        public DateTime InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime UpdDatetime { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
