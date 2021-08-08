using System;

namespace ProductStore.RestfulAPI.Repositories.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        void Complete();
        void CreateTransaction();
        void Commit();
        void Rollback();
    }
}