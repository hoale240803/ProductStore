using ProductStore.RestfulAPI.Data;
using ProductStore.RestfulAPI.Repositories.Infrastructure;

namespace ProductStore.RestfulAPI.Repositories
{
    public interface ITagsRepository : IRepository<TagsEntity>
    {
    }

    public class TagsRepository : RepositoryBase<TagsEntity>, ITagsRepository
    {
     

        public TagsRepository(ProductStoreDbContext _dbContext) : base(_dbContext)
        {
        }
    }
}