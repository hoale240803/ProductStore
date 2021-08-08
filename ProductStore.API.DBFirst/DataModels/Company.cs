using System;
using System.Collections.Generic;

#nullable disable

namespace ProductStore.API.DBFirst.DataModels
{
    public partial class Company
    {
        public Company()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PersonInCharge { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
