using ProductStore.API.DBFirst.ViewModels.Product;
using System.Collections.Generic;

namespace ProductStore.API.DBFirst.DataModels.EmployeeVM
{
    public class EmployeeGroupByCategory
    {
        public string  EmployeeCategory { get; set; }
    }

    public class EmployeeCategory
    {
        public string EmployeeCategoryProduct { get; set; }
    }

    public class EmployeeCategoryProduct
    {
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}