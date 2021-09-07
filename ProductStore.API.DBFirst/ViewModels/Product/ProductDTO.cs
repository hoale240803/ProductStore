using ProductStore.API.DBFirst.ViewModels.OrdersProduct;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.API.DBFirst.ViewModels.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [NotMapped]
        public int IdCategory { get; set; }

        [NotMapped]
        public int? IdTransporter { get; set; }

        [NotMapped]
        public string IdMaterials { get; set; }

        [NotMapped]
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

        [NotMapped]
        public IEnumerable<OrdersProductDTO> OrdersProductsDTO { get; set; }
    }
}