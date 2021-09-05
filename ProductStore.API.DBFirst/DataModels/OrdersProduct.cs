using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProductStore.API.DBFirst.DataModels
{
    public partial class OrdersProduct
    {
        [Key]
        public int IdOrder { get; set; }

        public int IdProduct { get; set; }
        public int Quantity { get; set; }

        public virtual Order IdOrderNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
    }
}