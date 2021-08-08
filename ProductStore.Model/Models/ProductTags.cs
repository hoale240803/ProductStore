using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Model.Models
{
    [Table("ProductTags")]
    public class ProductTags
    {
        [Key]
        [Column(Order = 1)]
        public int ProductID { set; get; }

        [Key]
        [Column(Order = 2, TypeName = "varchar")]
        [MaxLength(50)]
        public string TagID { set; get; }

        [ForeignKey("ProductID")]
        public Products Product { set; get; }

        [ForeignKey("TagID")]
        public Tags Tag { set; get; }
    }
}