using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerShopAPI.Models
{
    public partial class PhotoProduct
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("photo")]
        public string Photo { get; set; }
        [Column("productId")]
        [StringLength(8)]
        public string ProductId { get; set; }
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

        [ForeignKey("ProductId")]
        [InverseProperty("PhotoProduct")]
        public virtual Product Product { get; set; }
    }
}
