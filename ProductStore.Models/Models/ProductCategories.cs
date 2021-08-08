using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Models.Models
{

    [Table("ProductCategory")]
    public class ProductCategories
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
        public virtual IEnumerable<Products> Product { set; get; }

    }
}