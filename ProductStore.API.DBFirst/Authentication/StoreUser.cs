using Microsoft.AspNetCore.Identity;
using ProductStore.API.DBFirst.DataModels;
using System.Collections.Generic;

namespace ProductStore.API.DBFirst.Authentication
{
    public class StoreUser : IdentityUser
    {
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}