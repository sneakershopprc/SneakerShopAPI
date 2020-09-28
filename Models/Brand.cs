﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerShopAPI.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Product = new HashSet<Product>();
        }

        [Column("brandId")]
        [StringLength(8)]
        public string BrandId { get; set; }
        [Required]
        [Column("brandNm")]
        [StringLength(200)]
        public string BrandNm { get; set; }
        [Required]
        [Column("photo")]
        [StringLength(300)]
        public string Photo { get; set; }
        [Column("description")]
        [StringLength(500)]
        public string Description { get; set; }
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

        [InverseProperty("Brand")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
