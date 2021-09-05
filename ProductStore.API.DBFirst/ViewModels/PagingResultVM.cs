using System.Collections.Generic;

namespace ProductStore.API.DBFirst.ViewModels.PagingResult
{
    public class PagingResultVM<T>
    {
        public string Keyword { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int PageRange { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}