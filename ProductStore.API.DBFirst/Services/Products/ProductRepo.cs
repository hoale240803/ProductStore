using ProductStore.API.DBFirst.DataModels;
using ProductStore.API.DBFirst.DataModels.EmployeeVM;
using ProductStore.API.DBFirst.Services.Infrastructure;
using ProductStore.API.DBFirst.ViewModels;
using ProductStore.API.DBFirst.ViewModels.QueryString;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.Services.Products
{
    public class ProductRepo : RepositoryBase<Product>, IProductServices
    {
        private StoreContext DbContext;

        public ProductRepo(StoreContext dbContext) : base(dbContext)
        {
            this.DbContext = dbContext;
        }

        public void CreateProduct(Product Product)
        {
            Add(Product);
        }

        //public void CreateMultiProduct(List<Product> productList)
        //{
        //    Add(productList);
        //}
        public void DeleteProduct(Product Product)
        {
            Delete(Product);
        }

        public void UpdateProduct(Product Product)
        {
            Update(Product);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await GetSingleById(productId);
        }

        public async Task SaveAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public async Task ExportWord()
        {
        }

        public async Task ExportCSV()
        {
            //var productList = GetAll();
            //var builder = new StringBuilder();
            //builder.AppendLine("Id,Username");
            //foreach (var product in productList)
            //{
            //    builder.AppendLine($"{product.Id},{product.Name}");
            //}

            //return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "users.csv");
        }

        public async Task ExportPDF()
        {
        }

        public async Task ExportExcel()
        {
        }

        public async Task ImportWord()
        {
        }

        public async Task ImportPDF()
        {
        }

        public async Task ImportExcel()
        {
        }

        public async Task<EmployeeGroupByCategory> GetProductByEmployeeCategory()
        {
            var query = from emp in DbContext.Employees
                        join prod in DbContext.Products on emp.Id equals prod.Id
                        join categ in DbContext.Categories on prod.Id equals categ.Id
                        join img in DbContext.Medias on categ.Id equals img.Id
                        select new { prod, categ, img, emp };
            //var res = classAttendanceByCriteria.OrderBy(x => x.Lecture.Id)
            //                       .GroupBy(x => new { x.Lecture, x.Teacher })
            //                       .Select(x => new { x.Key.Lecture, x.Key.Teacher })
            //                       .ToList();

            var filter = query.GroupBy(x => new EmployeeGroupByCategory { EmployeeCategory = x.emp.Name })
                        .GroupBy(x => new EmployeeCategory { EmployeeCategoryProduct = x.Key.EmployeeCategory })
                        .ToList();

            EmployeeGroupByCategory e = new EmployeeGroupByCategory();
            //e.EmployeeCategory= filter.

            return e;
        }

        public PagedList<Product> GetProducts(ProductParameters productParameters)
        {
            return PagedList<Product>.ToPagedList(DbContext.Products.AsQueryable(),
                productParameters.PageNumber,
                productParameters.PageSize);
        }

        //public async IQueryable<Product> GetAllProductByPaging(int pageSize, int startIndex)
        //{
        //    int skipCount = (startIndex - 1) * pageSize;
        //    IQueryable<Product> _resetSet;
        //    _resetSet = _dbContext.Products.AsQueryable().Skip(skipCount).Take(pageSize);

        //    //var queryResult = from p in _dbContext.Products
        //    //                  join pt in _dbContext.ProductTags on p.Id equals pt.ProductID
        //    //                  join t in _dbContext.tag on pt.TagID equals t.ID
        //    //                  select new { p.Alias, p.Name, p.Price, t };
        //    return _resetSet.AsQueryable();
        //}
    }
}