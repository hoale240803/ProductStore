using ProductStore.API.DBFirst.DataModels;
using ProductStore.API.DBFirst.Services.Infrastructure;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.Services.Products
{
    public interface IProductServices : IRepository<Product>
    {
        //Task<IEnumerable<Product>> GetAllProducts();
        //Task<IEnumerable<Product>> GetAllProductsAndPaging(Expression<Func<Product, bool>> predicate, out int total, int index = 0, int size = 50, string[] includes = null);
        Task<Product> GetProductByIdAsync(int ProductId);

        //Task<Product> GetProductWithDetails(int ProductId);
        void CreateProduct(Product Product);

        void UpdateProduct(Product Product);

        void DeleteProduct(Product Product);

        Task SaveAsync();

        public Task ExportWord();

        public Task ExportPDF();

        public Task ExportExcel();

        public Task ImportWord();

        public Task ImportPDF();

        public Task ImportExcel();
    }
}