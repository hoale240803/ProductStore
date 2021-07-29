﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Model.Models
{
    public class OrderDetails
    {
        [Key]
        [Column(Order = 1)]
        public int OrderID { set; get; }
        [Key]
        [Column(Order = 2)]
        public int ProductID { set; get; }
        [ForeignKey("ProductID")]
        public virtual Products Product {set;get;}
        [ForeignKey("OrderID")]
        public virtual Products Order { set; get; }
        public int Quantity { set; get; }
    }
}