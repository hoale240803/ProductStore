using System;

namespace ProductStore.RestfulAPI.Repositories.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ProductStoreDbContext Init();
    }
}