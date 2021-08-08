using ProductStore.RestfulAPI.Data.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStore.RestfulAPI.ViewModel
{
    public class ProductsVM : Editable
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public string Alias { set; get; }

        public string MoreImages { set; get; }
        public decimal? Price { set; get; }
        public decimal? OriginalPrice { set; get; }
        public int? Stock { set; get; }
        public int? ViewCount { set; get; }

        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
        public bool? IsFeatured { get; set; }

        public string Image { get; set; }


        public List<string> ThumbnailImage { get; set; }

        public int? CategoryID { get; set; }


        public virtual ProductCategoriesVM ProductCategory { get; set; }
    }
}