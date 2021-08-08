namespace ProductStore.Datas.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}