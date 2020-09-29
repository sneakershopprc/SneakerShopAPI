using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.ViewModels
{
    public class WishListVModel
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public ProductVModel Product { get; set; }
        public string Username { get; set; }
    }
}
