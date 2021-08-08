using ProductStore.RestfulAPI.Data.Products;
using ProductStore.RestfulAPI.Repositories.Infrastructure;

namespace ProductStore.RestfulAPI.Repositories
{
    public interface IProductTagsRepository : IRepository<ProductTagsEntity>
    {
    }

    public class ProductTagsRepository : RepositoryBase<ProductTagsEntity>, IProductTagsRepository
    {
     

        public ProductTagsRepository(ProductStoreDbContext _dbContext) : base(_dbContext)
        {
        }
    }
}