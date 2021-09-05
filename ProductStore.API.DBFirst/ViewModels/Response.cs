using Microsoft.AspNetCore.Identity;
using ProductStore.API.DBFirst.ViewModels.PagingResult;
using System.Collections.Generic;

namespace ProductStore.API.DBFirst.DataModels.Models
{
    public class Response<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T Content { get; set; }

        public IEnumerable<T> Contents { get; set; }
        public IEnumerable<IdentityError> ListMessage { get; set; }

        public PagingResultVM<T> Results { get; set; }

    }
}