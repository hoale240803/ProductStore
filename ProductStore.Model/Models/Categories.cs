using System.Collections.Generic;

namespace ProductStore.Model.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public virtual IEnumerable<Products> ListProduct { get; set; }
    }
}