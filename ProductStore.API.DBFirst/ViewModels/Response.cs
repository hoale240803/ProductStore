using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ProductStore.API.DBFirst.DataModels.Models
{
    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public IEnumerable<IdentityError> ListMessage { get; set; }
    }
}