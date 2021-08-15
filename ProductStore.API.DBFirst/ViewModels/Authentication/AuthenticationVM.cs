using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProductStore.API.DBFirst.ViewModels.Authentication
{
    public class AuthenticationVM
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }

        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}