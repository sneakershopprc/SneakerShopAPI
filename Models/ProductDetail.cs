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
        [Column("size", TypeName = "decimal(18, 0)")]
        public decimal Size { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price", TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("ProductDetail")]
        public virtual Product Product { get; set; }
    }
}
