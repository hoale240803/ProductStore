using ProductStore.Model.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Model.Models
{
    [Table("Order")]
    public class Orders : Editable
    {
        [Key]
        public int ID { set; get; }

        [MaxLength(256)]
        public string CustomerName { set; get; }

        [MaxLength(256)]
        public string CustomerAddress { set; get; }

        [MaxLength(256)]
        public string CustomerEmail { set; get; }

        [MaxLength(20)]
        public string CustomerMobile { set; get; }

        [MaxLength(500)]
        public string CustomerMessage { set; get; }

        [MaxLength(256)]
        public string PaymentMethod { set; get; }

        [MaxLength(256)]
        public string PaymentState { set; get; }

        public virtual IEnumerable<OrderDetails> OrderDetails { set; get; }
    }
}