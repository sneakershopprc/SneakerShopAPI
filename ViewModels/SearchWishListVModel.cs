using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.ViewModels
{
    public class SearchWishListVModel : ResourceParameters
    {
        public string Username { get; set; }
    }
}
