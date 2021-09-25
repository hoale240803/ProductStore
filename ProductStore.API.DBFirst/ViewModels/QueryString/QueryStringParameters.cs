namespace ProductStore.API.DBFirst.ViewModels.QueryString
{
    public abstract class QueryStringParameters
    {
        public string Keyword { get; set; }
        private const int maxPageSize = 50;
        private int _pageNumber = 1;

        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageSize = (value < _pageNumber) ? _pageNumber : value;
            }
        }

        private int _pageSize = 15;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}