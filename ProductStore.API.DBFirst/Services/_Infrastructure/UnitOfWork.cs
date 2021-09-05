using ProductStore.API.DBFirst.DataModels;

namespace ProductStore.API.DBFirst.Services.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private StoreContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public StoreContext DbContext
        {
            get { return dbContext ??= dbFactory.Init(); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}