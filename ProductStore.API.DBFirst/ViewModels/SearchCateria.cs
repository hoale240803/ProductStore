using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.ViewModels
{
    public class SearchCateria
    {
        public string Keyword { get; set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }

        public int PageNumber { get; private set; }

    }
}
