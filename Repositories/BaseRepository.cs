using SneakerShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly SneakerShopContext context;
        protected BaseRepository(SneakerShopContext _context)
        {
            context = _context;
        }
    }
}
