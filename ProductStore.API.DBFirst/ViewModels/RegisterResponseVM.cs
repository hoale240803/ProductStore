using System.Collections.Generic;

namespace ProductStore.API.DBFirst.ViewModels
{
    public class RegisterResponseVM
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public IEnumerable<IdentityError> ListMessage { get; set; }
        public string Token { get; set; }
    }
}