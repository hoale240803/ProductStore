namespace ProductStore.Data.Infrastructure
{
    internal interface IUnitOfWork
    {
        void Commit();
    }
}