using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.RestfulAPI.Data
{
    [Table("PostTags")]
    public class PostTagsEntity
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
        public virtual TagsEntity Tag { set; get; }
    }
}