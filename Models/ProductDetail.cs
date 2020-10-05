using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerShopAPI.Models
{
    public partial class ProductDetail
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("productId")]
        [StringLength(8)]
        public string ProductId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price")]
        public double Price { get; set; }
        [Column("size")]
        public double Size { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("ProductDetail")]
        public virtual Product Product { get; set; }
    }
}
