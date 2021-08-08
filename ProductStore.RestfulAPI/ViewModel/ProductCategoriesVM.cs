using System.Collections.Generic;

namespace ProductStore.RestfulAPI.ViewModel
{
    public class ProductCategoriesVM
    {
        public int ID { set; get; }

        public string Alias { set; get; }

        public string Name { set; get; }

        public virtual IEnumerable<ProductsVM> Product { set; get; }
    }
}