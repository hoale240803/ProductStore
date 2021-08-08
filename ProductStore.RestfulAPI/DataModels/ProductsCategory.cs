using System;
using System.Collections.Generic;

#nullable disable

namespace ProductStore.RestfulAPI.DataModels
{
    public partial class ProductsCategory
    {
        public ProductsCategory()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
