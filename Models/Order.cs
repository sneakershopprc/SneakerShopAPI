using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerShopAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        [Column("orderId")]
        [StringLength(8)]
        public string OrderId { get; set; }
        [Required]
        [Column("username")]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [Column("shippingAddress")]
        [StringLength(500)]
        public string ShippingAddress { get; set; }
        [Required]
        [Column("receiverNm")]
        [StringLength(200)]
        public string ReceiverNm { get; set; }
        [Required]
        [Column("phonenumber")]
        [StringLength(10)]
        public string Phonenumber { get; set; }
        [Required]
        [Column("status")]
        [StringLength(50)]
        public string Status { get; set; }
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
        [InverseProperty("Order")]
        public virtual Account UsernameNavigation { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
