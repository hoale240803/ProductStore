using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore.RestfulAPI.Data.Products
{
    public class ProductImageEntity
    {
        public string ID { get; set; }
        public string FileID { get; set;}
        public string ExternalUrl { get; set; }
        public string FileName { get; set; }
    }
}
