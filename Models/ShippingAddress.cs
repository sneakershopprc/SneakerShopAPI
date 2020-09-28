using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerShopAPI.Models
{
    public partial class ShippingAddress
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("address")]
        [StringLength(500)]
        public string Address { get; set; }
        [Column("phonenumber")]
        [StringLength(11)]
        public string Phonenumber { get; set; }
        [Column("isDefault")]
        public bool IsDefault { get; set; }
        [Required]
        [Column("username")]
        [StringLength(50)]
        public string Username { get; set; }
        [Column("delFlg")]
        public bool DelFlg { get; set; }
        [Required]
        [Column("insBy")]
        [StringLength(50)]
        public string InsBy { get; set; }
        [Column("insDatetime", TypeName = "datetime")]
        public DateTime InsDatetime { get; set; }
        [Required]
        [Column("updBy")]
        [StringLength(50)]
        public string UpdBy { get; set; }
        [Column("updDatetime", TypeName = "datetime")]
        public DateTime UpdDatetime { get; set; }

        [ForeignKey("Username")]
        [InverseProperty("ShippingAddress")]
        public virtual Account UsernameNavigation { get; set; }
    }
}
