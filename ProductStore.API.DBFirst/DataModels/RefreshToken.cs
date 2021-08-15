using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProductStore.API.DBFirst.DataModels
{
    public partial class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }

        // condition DateTime.UtcNow >= Expires;
        public bool IsExpired { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }

        // condition Revoked == null && !IsExpired;
        public bool IsActive { get; set; }

        //public bool IsUsed { get; set; }
    }
}