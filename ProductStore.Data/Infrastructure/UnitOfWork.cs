namespace ProductStore.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private ProductStoreDbContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public ProductStoreDbContext DbContext
        {
            get { return dbContext ?? dbFactory.Init(); }


        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}