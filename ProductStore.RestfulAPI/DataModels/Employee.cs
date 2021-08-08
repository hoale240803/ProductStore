using System;
using System.Collections.Generic;

#nullable disable

namespace ProductStore.RestfulAPI.DataModels
{
    public partial class Employee
    {
        public Employee()
        {
            Media = new HashSet<Media>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? StartedDate { get; set; }
        public int? IdMedia { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
