namespace ProductStore.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}