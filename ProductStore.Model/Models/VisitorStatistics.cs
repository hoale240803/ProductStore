using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Model.Models
{
    [Table("VisitorStatistics")]
    public class VisitorStatistics
    {
        [Key]
        public Guid ID { set; get; }
        [Required]
        public DateTime VisitedDate { set; get; }
        [MaxLength(50)]
        public string IPAddress { set; get; }
    }
}