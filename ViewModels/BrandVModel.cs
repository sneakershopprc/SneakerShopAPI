using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.ViewModels
{
    public class BrandVModel 
    {
        public string BrandId { get; set; }
        public string BrandNm { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string Implementer { get; set; }
    }
}
