using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProductStore.API.DBFirst.DataModels
{
    public partial class Order
    {
        public Order()
        {
            OrdersProducts = new HashSet<OrdersProduct>();
        }

        [Key]
        public int Id { get; set; }

        public int? IdPaymentMethod { get; set; }
        public int? IdInternalShipper { get; set; }
        public int? IdEmployee { get; set; }
        public string OrderCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? IdExternalShipper { get; set; }
        public string Status { get; set; }
        public string CreateBy { get; set; }

        public virtual Employee IdEmployeeNavigation { get; set; }
        public virtual ExternalShipper IdExternalShipperNavigation { get; set; }
        public virtual InternalShipper IdInternalShipperNavigation { get; set; }
        public virtual ICollection<OrdersProduct> OrdersProducts { get; set; }
    }
}