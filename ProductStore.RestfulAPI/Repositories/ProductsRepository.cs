using ProductStore.RestfulAPI.Data.Products;
using ProductStore.RestfulAPI.Repositories.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ProductStore.RestfulAPI.Repositories
{
    public interface IProductRepository : IRepository<ProductsEntity>
    {
        IEnumerable<ProductsEntity> GetAllByPaging(int pageSize, int startIndex);
    }

    public class ProductsRepository : RepositoryBase<ProductsEntity>, IProductRepository
    {
        private ProductStoreDbContext _dbContext;

        public ProductsRepository(ProductStoreDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProductsEntity> GetAllByPaging(int pageSize, int startIndex)
        {
            int skipCount = (startIndex - 1) * pageSize;
            IEnumerable<ProductsEntity> _resetSet;
            _resetSet = _dbContext.Products.AsEnumerable().Skip(skipCount).Take(pageSize);
            

            var queryResult = from p in _dbContext.Products
                              join pt in _dbContext.ProductTags on p.Id equals pt.ProductID
                              join t in _dbContext.Tags on pt.TagID equals t.ID
                              select new { p.Alias, p.Name, p.Price, t };
            return _resetSet.AsEnumerable();
        }   
    }
}