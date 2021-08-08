using System;

namespace ProductStore.RestfulAPI.ViewModel
{
    public class VisitorStatisticsVM
    {
        public Guid ID { set; get; }

        public DateTime VisitedDate { set; get; }

        public string IPAddress { set; get; }
    }
}