using ProductStore.Model;
using System;

namespace ProductStore.Datas.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ProductStoreDbContext Init();
    }
}