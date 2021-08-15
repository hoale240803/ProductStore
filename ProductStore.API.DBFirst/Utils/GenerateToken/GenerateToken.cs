using ProductStore.API.DBFirst.DataModels;
using System;
using System.Security.Cryptography;

namespace ProductStore.API.DBFirst.Utils.GenerateToken
{
    public class GenerateToken
    {
        public static RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddMinutes(3),
                    Created = DateTime.UtcNow
                };
            }
        }
    }
}