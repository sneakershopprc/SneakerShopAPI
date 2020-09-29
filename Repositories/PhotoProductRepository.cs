using SneakerShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.Repositories
{
    public class PhotoProductRepository : BaseRepository
    {

        public PhotoProductRepository(SneakerShopContext context) : base(context)
        {
        }

        public List<string> GetAll(string productId)
        {
            var photoList = context.PhotoProduct.Where(d => (d.DelFlg == false)
                        && d.ProductId == productId)
                    .Select(s => s.Photo).ToList();
            return photoList;
        }


        public bool Create(PhotoProduct productPhoto)
        {
            context.PhotoProduct.Add(productPhoto);
            return true;
        }

        public bool DeleteByProductId(string productId, string implementer)
        {
            List<PhotoProduct> productPhotoList = context.PhotoProduct.Where(d => d.ProductId == productId).ToList();
            foreach (PhotoProduct photoProduct in productPhotoList)
            {
                photoProduct.DelFlg = true;
                photoProduct.UpdBy = implementer;
                photoProduct.UpdDatetime = DateTime.Now;
            }
            return true;
        }

    }
}
