using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.RestfulAPI.Data.Products
{
    [Table("ProductTags")]
    public class ProductTagsEntity
    {
        [Key]
        [Column(Order = 1)]
        public int ProductID { set; get; }

        [Key]
        [Column(Order = 2, TypeName = "varchar")]
        [MaxLength(50)]
        public string TagID { set; get; }

        [ForeignKey("ProductID")]
        public ProductsEntity Product { set; get; }

        [ForeignKey("TagID")]
        public TagsEntity Tag { set; get; }
    }
}