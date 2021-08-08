using ProductStore.Model;

namespace ProductStore.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ProductStoreDbContext dbContext;

        public ProductStoreDbContext Init()
        {
            return dbContext==null ? dbContext : (new ProductStoreDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}