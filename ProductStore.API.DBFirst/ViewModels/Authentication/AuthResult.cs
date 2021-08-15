using System.Collections.Generic;

namespace ProductStore.API.DBFirst.ViewModels.Authentication
{
    public class AuthResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}