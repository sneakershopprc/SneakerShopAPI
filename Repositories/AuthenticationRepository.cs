using SneakerShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.Repositories
{
    public class AuthenticationRepository
    {
        private readonly SneakerShopContext context;

        public AuthenticationRepository(SneakerShopContext context)
        {
            this.context = context;
        }

        public string Get()
        {
            Brand b = context.Brand.Find("1");
            return b?.BrandNm;
        }
    }
}
