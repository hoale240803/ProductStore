using System.Data.Entity;

namespace ProductStore.Data.Infrastructure
{
    internal class ProductStoreDbContext : DbContext
    {
        public ProductStoreDbContext() : base("")
        {
        }
    }
}