using ProductStore.API.DBFirst.ViewModels.Product;
using System.Collections.Generic;

namespace ProductStore.API.DBFirst.DataModels.ProductVM
{
    public class ProductGroupByShipper
    {
        public TransporterCategory TransporterCategory { get; set; }
    }

    public class TransporterCategory
    {
        public TransporterCategoryProduct TransporterCategoryProducts { get; set; }
    }

    public class TransporterCategoryProduct
    {
        public List<ProductDTO> Products { get; set; }
    }
}