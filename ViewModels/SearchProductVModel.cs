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
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public List<String> BrandList { get; set; }
        public string BrandNm { get; set; }
        public int isStill { get; set; }

        public SearchProductVModel()
        {
            MinPrice = 0;
            MaxPrice = 99999;
        }
    }
}
