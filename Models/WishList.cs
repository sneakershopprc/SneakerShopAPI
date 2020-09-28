using System;
using System.Collections.Generic;

namespace SneakerShopAPI.Models
{
    public partial class WishList
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string Username { get; set; }
        public bool DelFlg { get; set; }
        public string InsBy { get; set; }
        public DateTime InsDatetime { get; set; }
        public string UpdBy { get; set; }
        public DateTime UpdDatetime { get; set; }

        public virtual Product Product { get; set; }
        public virtual Account UsernameNavigation { get; set; }
    }
}
