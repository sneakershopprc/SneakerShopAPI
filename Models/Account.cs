using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerShopAPI.Models
{
    public partial class Account
    {
        public Account()
        {
            Order = new HashSet<Order>();
            ShippingAddress = new HashSet<ShippingAddress>();
            WishList = new HashSet<WishList>();
        }

        [Column("username")]
        [StringLength(50)]
        public string Username { get; set; }
        [Column("passwordHash")]
        public byte[] PasswordHash { get; set; }
        [Column("passwordSalt")]
        public byte[] PasswordSalt { get; set; }
        [Column("fullname")]
        [StringLength(200)]
        public string Fullname { get; set; }
        [Column("email")]
        [StringLength(320)]
        public string Email { get; set; }
        [Column("photo")]
        public string Photo { get; set; }
        [Column("role")]
        [StringLength(50)]
        public string Role { get; set; }
        [Column("delFlg")]
        public bool? DelFlg { get; set; }
        [Column("insBy")]
        [StringLength(50)]
        public string InsBy { get; set; }
        [Column("insDatetime", TypeName = "datetime")]
        public DateTime? InsDatetime { get; set; }
        [Column("updBy")]
        [StringLength(50)]
        public string UpdBy { get; set; }
        [Column("updDatetime", TypeName = "datetime")]
        public DateTime? UpdDatetime { get; set; }

        [InverseProperty("UsernameNavigation")]
        public virtual ICollection<Order> Order { get; set; }
        [InverseProperty("UsernameNavigation")]
        public virtual ICollection<ShippingAddress> ShippingAddress { get; set; }
        [InverseProperty("UsernameNavigation")]
        public virtual ICollection<WishList> WishList { get; set; }
    }
}
