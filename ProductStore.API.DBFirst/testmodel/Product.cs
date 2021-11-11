using System;
using System.Collections.Generic;

#nullable disable

namespace ProductStore.API.DBFirst.testmodel
{
    public partial class Product
    {
        public Product()
        {
            Media = new HashSet<Media>();
            OrdersProducts = new HashSet<OrdersProduct>();
        }

        public int Id { get; set; }
        public int? IdCategory { get; set; }
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

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual Company IdCompanyNavigation { get; set; }
        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<OrdersProduct> OrdersProducts { get; set; }
    }
}
