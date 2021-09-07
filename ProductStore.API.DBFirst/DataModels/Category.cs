using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProductStore.API.DBFirst.DataModels
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdCategoryParrent { get; set; }
        public string Alias { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
