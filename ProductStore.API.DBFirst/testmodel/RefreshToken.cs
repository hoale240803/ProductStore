using System;
using System.Collections.Generic;

#nullable disable

namespace ProductStore.API.DBFirst.testmodel
{
    public partial class RefreshToken
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive { get; set; }
        public string StoreUserId { get; set; }

        public virtual AspNetUser StoreUser { get; set; }
    }
}
