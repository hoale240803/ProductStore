using ProductStore.Datas.Infrastructure;
using ProductStore.Models.Models;

namespace ProductStore.Data.Repositories
{
    public interface IOrdersRepository : IRepository<Orders>
    {
    }

    public class OrdersRepository : RepositoryBase<Orders>, IOrdersRepository
    {
        public OrdersRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}