using ProductStore.Data.Infrastructure;
using ProductStore.Model.Models;

namespace ProductStore.Data.Repositories
{
    public interface IProductRepository : IRepository<Products>
    {
        //IEnumerable<Products>
    }

    public class ProductsRepository : RepositoryBase<Products>, IProductRepository
    {
        public ProductsRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}