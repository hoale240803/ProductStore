using System;
using System.Collections.Generic;

#nullable disable

namespace ProductStore.API.DBFirst.testmodel
{
    public partial class ExportProductToExcel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Transporter { get; set; }
        public string Material { get; set; }
        public string Company { get; set; }
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
    }
}
