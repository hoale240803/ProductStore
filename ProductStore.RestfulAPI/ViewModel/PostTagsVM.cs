namespace ProductStore.RestfulAPI.ViewModel
{
    public class PostTagsVM
    {
        public int PostID { set; get; }

        public string TagID { set; get; }

        public virtual PostsVM Post { set; get; }

        public virtual TagsVM Tag { set; get; }
    }
}