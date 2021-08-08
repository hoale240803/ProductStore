using ProductStore.RestfulAPI.Data;
using ProductStore.RestfulAPI.Repositories.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.RestfulAPI.Repositories

{
    public interface IProductCategoryRepository:IRepository<ProductCategoryEntity>
    {

    }
    public class ProductCategoryRepository:RepositoryBase<ProductCategoryEntity>, IProductCategoryRepository
    {
        private ProductStoreDbContext _dbContext;

        public ProductCategoryRepository(ProductStoreDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }



    }
}
