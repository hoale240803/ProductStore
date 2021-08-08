using ProductStore.RestfulAPI.Data.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.RestfulAPI.Data
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
        public virtual ProductsEntity Product { set; get; }
        [ForeignKey("OrderID")]
        public virtual Orders Order { set; get; }
        public int Quantity { set; get; }
    }
}