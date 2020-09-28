﻿using SneakerShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.ViewModels
{
    public class ProductVModel
    {
        public string ProductId { get; set; }
        public string ProductNm { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Discount { get; set; }
        public string BrandId { get; set; }
        public string BrandNm { get; set; }
        public List<string> photoList { get; set; }
        public List<ProductDetail> productDetailList { get; set; }
        public int isStill { get; set; }
        public string Implementer { get; set; }

    }
}
