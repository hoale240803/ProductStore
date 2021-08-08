using System.Collections.Generic;

namespace ProductStore.RestfulAPI.ViewModel
{
    public class PostCategoriesVM
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Alias { set; get; }

        public string Description { set; get; }
        public int? ParentID { set; get; }
        public int? DisplayOrder { set; get; }

        public string Image { set; get; }
        public bool? HomeFlag { set; get; }
        public virtual IEnumerable<PostsVM> Posts { set; get; }
    }
}