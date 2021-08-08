namespace ProductStore.RestfulAPI.ViewModel
{
    public class ProductTagsVM
    {
        public int ProductID { set; get; }

        public string TagID { set; get; }

        public ProductsVM Product { set; get; }

        public TagsVM Tag { set; get; }
    }
}