using System;
using System.Collections.Generic;

#nullable disable

namespace ProductStore.RestfulAPI.DataModels
{
    public partial class InternalShipper
    {
        public InternalShipper()
        {
            Media = new HashSet<Media>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int? IdMedia { get; set; }
        public string Name { get; set; }
        public string PersonInCharge { get; set; }
        public string Phone { get; set; }
        public string Shipper { get; set; }
        public string ShipperPhoneNumber { get; set; }
        public string ShipperAddress { get; set; }
        public string Cmnd { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
