using System;
using System.Data.Entity;

namespace ProductStore.RestfulAPI.Repositories.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductStoreDbContext _dbContext;
        private DbContextTransaction _transaction;

        public UnitOfWork(ProductStoreDbContext dbContext, DbContextTransaction _transaction)
        {
            _dbContext = dbContext; 
        }

        public IProductRepository Product => throw new NotImplementedException();


        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Complete()
        {
            _dbContext.SaveChanges();
        }

        public void CreateTransaction()
        {
            _transaction = (DbContextTransaction)_dbContext.Database.BeginTransaction();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        void IUnitOfWork.Complete()
        {
            throw new NotImplementedException();
        }
    }
}