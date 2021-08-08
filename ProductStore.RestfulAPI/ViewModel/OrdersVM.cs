using System.Collections.Generic;

namespace ProductStore.RestfulAPI.ViewModel
{
    public class OrdersVM
    {
        public int ID { set; get; }

        public string CustomerName { set; get; }

        public string CustomerAddress { set; get; }

        public string CustomerEmail { set; get; }

        public string CustomerMobile { set; get; }

        public string CustomerMessage { set; get; }

        public string PaymentMethod { set; get; }

        public string PaymentState { set; get; }

        public virtual IEnumerable<OrderDetailsVM> OrderDetails { set; get; }
    }
}