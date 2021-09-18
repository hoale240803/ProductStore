using ProductStore.API.DBFirst.ViewModels.Product;
using System.Collections.Generic;

namespace ProductStore.API.DBFirst.ViewModels.ProductVM.TransporterVM
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
        public List<ProductDTO> productList { get; set; }
    }
}