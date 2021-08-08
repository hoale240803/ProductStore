namespace ProductStore.RestfulAPI.ViewModel
{
    public class OrderDetailsVM
    {
        public int OrderID { set; get; }

        public int ProductID { set; get; }

        public virtual ProductsVM Product { set; get; }

        public virtual OrdersVM Order { set; get; }
        public int Quantity { set; get; }
    }
}