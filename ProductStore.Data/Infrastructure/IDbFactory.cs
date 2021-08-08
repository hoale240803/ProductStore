using System;

namespace ProductStore.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ProductStoreDbContext Init();
    }
}