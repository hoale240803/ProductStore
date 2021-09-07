using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProductStore.API.DBFirst.DataModels
{
    public partial class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? StartedDate { get; set; }
        public int? IdMedia { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
