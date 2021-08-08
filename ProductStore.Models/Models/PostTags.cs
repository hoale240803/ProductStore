using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Models.Models
{
    [Table("PostTags")]
    public class PostTags
    {
        [Key]
        [Column(Order = 1)]
        public int PostID { set; get; }

        [Key]
        [Column(Order = 2, TypeName = "varchar")]
        public string TagID { set; get; }

        [ForeignKey("PostID")]
        public virtual Posts Post { set; get; }

        [ForeignKey("TagID")]
        public virtual Tags Tag { set; get; }
    }
}