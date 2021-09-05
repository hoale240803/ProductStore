using ProductStore.API.DBFirst.DataModels;
using System;

namespace ProductStore.API.DBFirst.Services.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        StoreContext Init();
    }
}