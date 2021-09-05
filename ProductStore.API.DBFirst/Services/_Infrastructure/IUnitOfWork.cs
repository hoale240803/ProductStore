namespace ProductStore.API.DBFirst.Services.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}