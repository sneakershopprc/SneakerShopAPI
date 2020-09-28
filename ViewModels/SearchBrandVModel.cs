using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.Repositories
{
    public class SearchBrandVModel : ResourceParameters
    {
        public string BrandNm { get; set; }
    }
}
