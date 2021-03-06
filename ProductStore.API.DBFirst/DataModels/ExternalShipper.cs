using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProductStore.API.DBFirst.DataModels
{
    public partial class ExternalShipper
    {
        public ExternalShipper()
        {
            Orders = new HashSet<Order>();
        }
        [Key]
        public int Id { get; set; }
        public int? IdMedia { get; set; }
        public string Name { get; set; }
        public string PersonInCharge { get; set; }
        public string Phone { get; set; }
        public string PersonInShipper { get; set; }
        public string ShipperPhoneNumber { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
