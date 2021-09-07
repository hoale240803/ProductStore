using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ProductStore.API.DBFirst.DataModels
{

    public partial class Product
    {
        public Product()
        {
            OrdersProducts = new HashSet<OrdersProduct>();
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdCategory { get; set; }
   
        public int? IdTransporter { get; set; }
        public string IdMaterials { get; set; }
        public int? IdCompany { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public int Stock { get; set; }
        public int? Weight { get; set; }
        public int? Width { get; set; }
        public int? Lenght { get; set; }
        public string Status { get; set; }
        public int? Height { get; set; }


        [ForeignKey("IdCategory")]
        public virtual Category IdCategoryNavigation { get; set; }
        [ForeignKey("IdCompany")]
        public virtual Company IdCompanyNavigation { get; set; }

        public virtual ICollection<OrdersProduct> OrdersProducts { get; set; }
    }
}