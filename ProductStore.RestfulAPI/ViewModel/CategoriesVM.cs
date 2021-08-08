
using System.Collections.Generic;
namespace ProductStore.RestfulAPI.ViewModel
{ 
    public class CategoriesVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public IEnumerable<ProductsVM> ListProduct { get; set; }
    }
}