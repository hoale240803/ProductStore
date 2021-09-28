using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductStore.API.DBFirst.DataModels;
using ProductStore.API.DBFirst.DataModels.Models;
using ProductStore.API.DBFirst.Services.Products;
using ProductStore.API.DBFirst.ViewModels.Product;
using ProductStore.API.DBFirst.ViewModels.QueryString;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IProductServices _productServices;
        private IMapper _mapper;
        //private ILoggerManager _logger;

        public ProductsController(StoreContext context, IProductServices productServices, IMapper mapper)
        {
            _context = context;
            _productServices = productServices;
            _mapper = mapper;
        }

        // Post: api/Products/SearchPaging
        [HttpPost("search/paging")]
        public IActionResult SearchProductsPaging(ProductParameters productParameters)
        {
            try
            {
                if (productParameters == null)
                {
                    return BadRequest();
                }
                //IEnumerable<ProductDTO> productResults = new List<ProductDTO>();
                //var productEntities = await _productServices.GetMultiPaging(x =>
                //x.Name.Contains(productParameters.Keyword)
                //|| x.Description.Contains(productParameters.Keyword),
                //out int total,
                //searchCateria.CurrentPage,
                //searchCateria.PageSize,
                //null).ToListAsync();

                //productResults = _mapper.Map<List<ProductDTO>>(productEntities);
                //searchCateria.Results = productResults;
                //searchCateria.TotalCount = total;

                //PagedList<Product>.ToPagedList( _productServices.GetMultiPaging(x => (x.Name.Contains(searchCateria.Keyword) || x.Description.Contains(searchCateria.Keyword),out int total, searchCateria.CurrentPage, searchCateria.PageSize), null,searchCateria.CurrentPage,
                //searchCateria.PageSize);
                var _result = _productServices.GetProducts(productParameters);
                return Ok(_result.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error"+ ex.Message);
            }
        
        }

        //// GET: api/Products
        //[HttpPost("getAll")]
        //public async Task<IActionResult> GetProducts(ProductParameters pagingResultVM)
        //{
        //    IEnumerable<ProductDTO> productResults = new List<ProductDTO>();
        //    try
        //    {
        //        //var productEntities = await _productServices.GetMultiPaging(null, out int total, pagingResultVM.CurrentPage, pagingResultVM.PageSize, null).ToListAsync();
        //        //productResults = _mapper.Map<IEnumerable<ProductDTO>>(productEntities);
        //        //pagingResultVM.Results = productResults;
        //        //pagingResultVM.TotalCount = total;
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response<ProductDTO> { Status = "Error", Message = ex.Message });
        //    }

        //    return Ok(new Response<ProductDTO> { Results = pagingResultVM, Status = "200", Message = "GET LIST PRODUCT SUCCESS" });
        //}

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            ProductDTO productResult = null;
            try
            {
                //GET PRODUCT INFO
                var productEntity = await _productServices.GetProductByIdAsync(id);
                //GET IMAGE INFO
                productResult = _mapper.Map<ProductDTO>(productEntity);
                if (productEntity == null)
                {
                    return Ok(new Response<ProductDTO> { Status = "200", Message = "NO RESULT" });
                }
            }
            catch (Exception ex)
            {
                StatusCode(StatusCodes.Status500InternalServerError, new Response<ProductDTO> { Status = "Error", Message = ex.Message });
            }
            return Ok(new Response<ProductDTO> { Status = "200", Message = $"GET PRODUCT WITH {id} SUCCESS", Content = productResult });
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            try
            {
                var propductEntity = _mapper.Map<Product>(product);
                _productServices.Update(propductEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!ProductExists(id))
                {
                    return NotFound(new Response<ProductDTO> { Message = $"NO PRODUCT {id} FOUND", Status = "200" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response<ProductDTO> { Status = "Error", Message = ex.Message });
                }
            }

            return Ok(new Response<ProductDTO> { Message = $"UPDATE PRODUCT {id} SUCCESS", Status = "200" });
        }

        // PUT: api/Products/UpdateMulti
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateMulti")]
        public async Task<IActionResult> PutMultiProduct(List<ProductDTO> product)
        {
            try
            {
                var propductEntities = _mapper.Map<IEnumerable<Product>>(product);
                _productServices.UpdateMultiById(propductEntities);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ProductDTO> { Status = "Error", Message = ex.Message });
            }

            return Ok(new Response<ProductDTO> { Message = $"UPDATE PRODUCTS SUCCESS", Status = "200" });
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostProduct(ProductDTO product)
        {
            try
            {   // MAPPER
                var propductEntity = _mapper.Map<Product>(product);
                //var imageProductEntites = propductEntity.Media;
                // SAVE IMAGE FILES TO CLOUD

                // SAVE IMAGE INFO

                //SAVE PRODUCT INFO
                _productServices.CreateProduct(propductEntity);
                await _productServices.SaveAsync();
            }
            catch (Exception ex)
            {
                if (ProductExists(product.Id))
                {
                    return Conflict(new Response<ProductDTO> { Message = $" PRODUCT {product.Id} IS EXIST", Status = "409" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response<ProductDTO> { Status = "Error", Message = ex.Message });
                }
            }
            return Ok(new Response<ProductDTO> { Message = $"CREATE PRODUCT SUCCESS", Status = "200" });
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostMulti")]
        public async Task<IActionResult> PostMultiProduct(List<ProductDTO> product)
        {
            try
            {   // MAPPER
                var propductEntities = _mapper.Map<List<Product>>(product);
                //var imageProductEntites = propductEntity.Media;
                // SAVE IMAGE FILES TO CLOUD

                // SAVE IMAGE INFO

                //SAVE PRODUCT INFO
                _productServices.AddMulti(propductEntities);
                await _productServices.SaveAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ProductDTO> { Status = "Error", Message = ex.Message });
            }
            return Ok(new Response<ProductDTO> { Message = $"CREATE PRODUCT SUCCESS", Status = "200" });
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                // FIND ID PRODUCT
                var productEntity = await _productServices.GetSingleById(id);
                if (productEntity == null)
                {
                    return NotFound(new Response<ProductDTO> { Message = $"NO PRODUCT {id} FOUND", Status = "404" });
                }
                //UPDATE isDeleted = true
                _productServices.Delete(productEntity);
                await _productServices.SaveAsync();
            }
            catch (Exception ex)
            {
                StatusCode(StatusCodes.Status500InternalServerError, new Response<ProductDTO> { Status = "Error", Message = ex.Message });
            }
            return Ok(new Response<ProductDTO> { Message = $"DELETE PRODUCT {id} SUCCESS", Status = "200" });
        }

        // DELETE: api/Products/deleteMulti
        [HttpDelete("deleteMulti")]
        public async Task<IActionResult> DeleteMultiProduct(List<ProductDTO> listProductDto)
        {
            try
            {
                if (listProductDto.Count > 0)
                {
                    var productEntities = _mapper.Map<List<Product>>(listProductDto);

                    productEntities.ForEach(product =>
                        {
                            _productServices.Delete(product);
                        }
                    );
                    await _productServices.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                StatusCode(StatusCodes.Status500InternalServerError, new Response<ProductDTO> { Status = "Error", Message = ex.Message });
            }
            return Ok(new Response<ProductDTO> { Message = $"DELETE PRODUCTS SUCCESS", Status = "200" });
        }

        [HttpGet("exportExcel")]
        public async Task<IActionResult> ExportToExcel()
        {
            var productList = await _productServices.GetMultiPaging(null, out int total).ToListAsync();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");

                // SET HEADER EXCEL FILE
                var currentRow = 1;
                const int IdCol = 1;
                const int NameCol = 2;
                const int CategoryCol = 3;
                const int TransporterCol = 4;
                const int MaterialCol = 5;
                const int CompanyCol = 6;
                const int CountryCol = 7;
                const int PriceCol = 8;
                const int DescriptionCol = 9;
                const int QuantityCol = 10;
                const int StockCol = 11;
                const int WeightCol = 12;
                const int WidthCol = 13;
                const int LenghtCol = 14;
                const int StatusCol = 15;
                const int HeightCol = 16;
                worksheet.Cell(currentRow, IdCol).Value = "Id";
                worksheet.Cell(currentRow, NameCol).Value = "Name";
                worksheet.Cell(currentRow, CategoryCol).Value = "Category";
                worksheet.Cell(currentRow, TransporterCol).Value = "Transporters";
                worksheet.Cell(currentRow, MaterialCol).Value = "Materials";
                worksheet.Cell(currentRow, CompanyCol).Value = "Company";
                worksheet.Cell(currentRow, CountryCol).Value = "Country";
                worksheet.Cell(currentRow, PriceCol).Value = "Price";
                worksheet.Cell(currentRow, DescriptionCol).Value = "Description";
                worksheet.Cell(currentRow, QuantityCol).Value = "Quantity";
                worksheet.Cell(currentRow, StockCol).Value = "Stock";
                worksheet.Cell(currentRow, WeightCol).Value = "Weight";
                worksheet.Cell(currentRow, WidthCol).Value = "Width";
                worksheet.Cell(currentRow, LenghtCol).Value = "Lenght";
                worksheet.Cell(currentRow, StatusCol).Value = "Status";
                worksheet.Cell(currentRow, HeightCol).Value = "Height";

                //SET DATA FOR EACH ROW FROM VIEW
                foreach (var user in productList)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, IdCol).Value = user.Id;
                    worksheet.Cell(currentRow, NameCol).Value = user.Name;
                    worksheet.Cell(currentRow, CategoryCol).Value = user.IdCategory;
                    worksheet.Cell(currentRow, TransporterCol).Value = user.IdTransporter;
                    worksheet.Cell(currentRow, MaterialCol).Value = user.IdMaterials;
                    worksheet.Cell(currentRow, CompanyCol).Value = user.IdCompany;
                    worksheet.Cell(currentRow, CountryCol).Value = user.Country;
                    worksheet.Cell(currentRow, PriceCol).Value = user.Price;
                    worksheet.Cell(currentRow, DescriptionCol).Value = user.Description;
                    worksheet.Cell(currentRow, QuantityCol).Value = user.Quantity;
                    worksheet.Cell(currentRow, StockCol).Value = user.Stock;
                    worksheet.Cell(currentRow, WeightCol).Value = user.Weight;
                    worksheet.Cell(currentRow, WidthCol).Value = user.Width;
                    worksheet.Cell(currentRow, LenghtCol).Value = user.Lenght;
                    worksheet.Cell(currentRow, StatusCol).Value = user.Status;
                    worksheet.Cell(currentRow, HeightCol).Value = user.Height;
                }

                // PUSH TO STREAM AND RETURN EXCEL FILE
                using (var stream = new System.IO.MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFile.xlsx");
                }
            }
        }

        [HttpGet("exportCSV")]
        public async Task<IActionResult> ExportToCSV()
        {
            var productList = await _productServices.GetMultiPaging(null, out int total).ToListAsync();
            var builder = new StringBuilder();
            builder.AppendLine("Id,Username");
            foreach (var product in productList)
            {
                builder.AppendLine($"{product.Id},{product.Name}");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "products.csv");
        }

        [HttpPost("importExcel")]
        public async Task<IActionResult> ImportExcel(IFormFile formFile)
        {
            if (formFile == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response<ProductDTO> { Status = "Error", Message = "No chosen file to upload" });
            }
            //get file name
            var filename = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');

            //get path
            var MainPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Files");

            //create directory "Uploads" if it doesn't exists
            if (!Directory.Exists(MainPath))
            {
                Directory.CreateDirectory(MainPath);
            }

            //get file path
            var filePath = Path.Combine(MainPath, formFile.FileName);
            using (System.IO.Stream stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            //get extension
            string extension = Path.GetExtension(filename);

            string conString = string.Empty;

            switch (extension)
            {
                case ".xls": //Excel 97-03.
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                    break;

                case ".xlsx": //Excel 07 and above.
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                    break;
            }

            using (XLWorkbook workbook = new XLWorkbook(filePath))
            {
                IXLWorksheet worksheet = workbook.Worksheet(1);
                bool FirstRow = true;
                //Range for reading the cells based on the last cell used.
                string readRange = "1:1";
                List<Product> producEntitytList = new List<Product>();
                foreach (IXLRow row in worksheet.RowsUsed())
                {
                    //If Reading the First Row (used) then add them as column name
                    if (FirstRow)
                    {
                        //Checking the Last cellused for column generation in datatable
                        readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber);
                        //foreach (IXLCell cell in row.Cells(readRange))
                        //{
                        //    var cellValue = cell.Value.ToString();
                        //    var cellValue2 = cell.Value.ToString();
                        //}
                        FirstRow = false;
                    }
                    else
                    {
                        //Adding a Row in datatable
                        //dt.Rows.Add();
                        int cellIndex = 1;
                        Product newProduct = new Product();
                        //Updating the values of datatable
                        foreach (IXLCell cell in row.Cells(readRange))
                        {
                            //dt.Rows[dt.Rows.Count - 1][cellIndex] = cell.Value.ToString();
                            var cellValue = cell.Value.ToString();
                            var cellValu1e = cell.Value.ToString();

                            //if (cellIndex==0)
                            //        newProduct.Id = Convert.ToInt32(cell.Value.ToString());
                            //    else if(cellIndex==1)
                            //        newProduct.Name = cell.Value.ToString();
                            const int IdCol = 1;
                            const int NameCol = 2;
                            const int CategoryCol = 3;
                            const int TransporterCol = 4;
                            const int MaterialCol = 5;
                            const int CompanyCol = 6;
                            const int CountryCol = 7;
                            const int PriceCol = 8;
                            const int DescriptionCol = 9;
                            const int QuantityCol = 10;
                            const int StockCol = 11;
                            const int WeightCol = 12;
                            const int WidthCol = 13;
                            const int LenghtCol = 14;
                            const int StatusCol = 15;
                            const int HeightCol = 16;

                            switch (cellIndex)
                            {
                                case IdCol:
                                    newProduct.Id = 0;
                                    break;

                                case NameCol:
                                    newProduct.Name = cell.Value.ToString();
                                    break;

                                case CategoryCol:
                                    newProduct.IdCategory = cell.Value.ToString() != "" ? Convert.ToInt32(cell.Value.ToString()) : 0;
                                    break;

                                case TransporterCol:
                                    newProduct.IdTransporter = cell.Value.ToString() != "" ? Convert.ToInt32(cell.Value.ToString()) : 0;
                                    break;

                                case MaterialCol:
                                    newProduct.IdMaterials = cell.Value.ToString();
                                    break;

                                case CompanyCol:
                                    var companyCell = await _context.Categories.FirstOrDefaultAsync(x => x.Name.Contains(cell.Value.ToString()));
                                    newProduct.IdCompany = companyCell != null ? companyCell.Id : 1;
                                    break;

                                case CountryCol:
                                    newProduct.Country = cell.Value.ToString();
                                    break;

                                case PriceCol:
                                    newProduct.Price = cell.Value.ToString() != "" ? Convert.ToInt32(cell.Value.ToString()) : 0;
                                    break;

                                case DescriptionCol:
                                    newProduct.Description = cell.Value.ToString();
                                    break;

                                case QuantityCol:
                                    newProduct.Quantity = cell.Value.ToString() != "" ? Convert.ToInt32(cell.Value.ToString()) : 0;
                                    break;

                                case StockCol:
                                    newProduct.Stock = cell.Value.ToString() != "" ? Convert.ToInt32(cell.Value.ToString()) : 0;
                                    break;

                                case WeightCol:
                                    newProduct.Weight = cell.Value.ToString() != "" ? Convert.ToInt32(cell.Value.ToString()) : 0;
                                    break;

                                case WidthCol:
                                    newProduct.Width = cell.Value.ToString() != "" ? Convert.ToInt32(cell.Value.ToString()) : 0;
                                    break;

                                case LenghtCol:
                                    newProduct.Lenght = cell.Value.ToString() != "" ? Convert.ToInt32(cell.Value.ToString()) : 0;
                                    break;

                                case StatusCol:
                                    newProduct.Status = cell.Value.ToString();
                                    break;

                                case HeightCol:
                                    newProduct.Height = cell.Value.ToString() != "" ? Convert.ToInt32(cell.Value.ToString()) : 0;
                                    break;
                            }

                            cellIndex = cellIndex + 1;
                        }
                        producEntitytList.Add(newProduct);
                    }
                }
                //If no data in Excel file
                if (FirstRow)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response<ProductDTO> { Status = "Error", Message = "File is empty!" });
                }
                _productServices.AddMulti(producEntitytList);
                //await _productServices.SaveAsync();
                await _productServices.SaveAsync();
            }
            return Ok(new Response<ProductDTO> { Status = "200", Message = "UPLOAD FILE SUCCESS" });
        }

        private bool ProductExists(int id)
        {
            try
            {
                return _productServices.CheckContains(e => e.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}