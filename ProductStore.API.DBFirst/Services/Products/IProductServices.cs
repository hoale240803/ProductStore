using ProductStore.API.DBFirst.DataModels;
using ProductStore.API.DBFirst.Services.Infrastructure;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.Services.Products
{
    public interface IProductServices : IRepository<Product>
    {
        //Task<IEnumerable<Product>> GetAllProductsAsync();
        //Task<IEnumerable<Product>> GetAllProductsAndPaging(Expression<Func<Product, bool>> predicate, out int total, int index = 0, int size = 50, string[] includes = null);
        Task<Product> GetProductByIdAsync(int ProductId);

        //Task<Product> GetProductWithDetailsAsync(int ProductId);
        void CreateProduct(Product Product);

        void UpdateProduct(Product Product);

        void DeleteProduct(Product Product);

        Task SaveAsync();
    }
}