using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerShopAPI.Models
{
    public partial class OrderDetail
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("orderId")]
        [StringLength(8)]
        public string OrderId { get; set; }
        [Column("product")]
        public string Product { get; set; }
        [Column("quantity")]
        public int? Quantity { get; set; }

        [ForeignKey("OrderId")]
        [InverseProperty("OrderDetail")]
        public virtual Order Order { get; set; }
    }
}
