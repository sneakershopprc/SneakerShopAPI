using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerShopAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            PhotoProduct = new HashSet<PhotoProduct>();
            ProductDetail = new HashSet<ProductDetail>();
            WishList = new HashSet<WishList>();
        }

        [Column("productId")]
        [StringLength(8)]
        public string ProductId { get; set; }
        [Required]
        [Column("productNm")]
        [StringLength(200)]
        public string ProductNm { get; set; }
        [Required]
        [Column("color")]
        [StringLength(50)]
        public string Color { get; set; }
        [Column("description")]
        [StringLength(500)]
        public string Description { get; set; }
        [Column("discount")]
        public double Discount { get; set; }
        [Column("brandId")]
        [StringLength(8)]
        public string BrandId { get; set; }
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

        [ForeignKey("BrandId")]
        [InverseProperty("Product")]
        public virtual Brand Brand { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<PhotoProduct> PhotoProduct { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductDetail> ProductDetail { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<WishList> WishList { get; set; }
    }
}
