using ProductStore.API.DBFirst.DataModels;

namespace ProductStore.API.DBFirst.Services.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private StoreContext dbContext;

        public StoreContext Init()
        {
            return dbContext ??= new StoreContext();
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}