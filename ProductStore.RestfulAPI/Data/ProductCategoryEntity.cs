using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.RestfulAPI.Data
{
    [Table("ProductCategory")]
    public class ProductCategoryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Alias { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        //[NotMapped]
        //public virtual IEnumerable<ProductsFull> Product { set; get; }
    }
}