using ProductStore.API.DBFirst.ViewModels.Product;
using System.Collections.Generic;

namespace ProductStore.API.DBFirst.ViewModels.ProductVM.TransporterVM
{
    public class EmployeeGroupByCategory
    {
        public EmployeeCategory EmployeeCategory { get; set; }
    }

    public class EmployeeCategory
    {
        public EmployeeCategoryProduct EmployeeCategoryProduct { get; set; }
    }

    public class EmployeeCategoryProduct
    {
        public List<ProductDTO> Products { get; set; }
    }
}