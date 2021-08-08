using Microsoft.EntityFrameworkCore;
using System;

namespace ProductStore.RestfulAPI.Repositories.Infrastructure
{
    public class DbFactory : IDisposable
    {
        private bool _disposed;

        // delagete only return result
        private readonly Func<ProductStoreDbContext> _instanceFunc;

        private DbContext _dbContext;
        public DbContext DbContext => _dbContext ?? (_dbContext = _instanceFunc.Invoke());

        public DbFactory(Func<ProductStoreDbContext> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}