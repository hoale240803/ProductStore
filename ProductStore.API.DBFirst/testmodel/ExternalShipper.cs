using System;
using System.Collections.Generic;

#nullable disable

namespace ProductStore.API.DBFirst.testmodel
{
    public partial class ExternalShipper
    {
        public ExternalShipper()
        {
            Media = new HashSet<Media>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PersonInCharge { get; set; }
        public string Phone { get; set; }
        public string PersonInShipper { get; set; }
        public string ShipperPhoneNumber { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
