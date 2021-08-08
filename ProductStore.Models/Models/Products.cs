
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.Models.Models
{

    [Table("Products")]
    public class Products 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        [Required]
        [MaxLength(256)]
        public string Alias { set; get; }
        [Column(TypeName = "xml")]
        public string MoreImages { set; get; }
        public decimal? Price { set; get; }
        public decimal? OriginalPrice { set; get; }
        public int? Stock { set; get; }
        public int? ViewCount { set; get; }
        [MaxLength(1000)]
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
        public bool? IsFeatured { get; set; }
        [Required]
        [MaxLength(256)]
        public string Image { get; set; }
        public IEnumerable<string> ThumbnailImage { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual ProductCategories ProductCategory { get; set; }
    }
}