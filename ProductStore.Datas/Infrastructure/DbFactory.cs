using ProductStore.Model;

namespace ProductStore.Datas.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ProductStoreDbContext dbContext;

        public ProductStoreDbContext Init()
        {
            return dbContext ??= new ProductStoreDbContext();
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}