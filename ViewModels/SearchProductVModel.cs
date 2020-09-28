using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.ViewModels
{
    public class SearchProductVModel : ResourceParameters
    {
        public string ProductNm { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string BrandId { get; set; }
        public string BrandNm { get; set; }
        public int isStill { get; set; }

    }
}
