using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductStore.API.DBFirst.DataModels;
using ProductStore.API.DBFirst.DataModels.Models;
using ProductStore.API.DBFirst.Services.Products;
using ProductStore.API.DBFirst.ViewModels.PagingResult;
using ProductStore.API.DBFirst.ViewModels.Product;
using System;
using System.Collections.Generic;
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

        public ProductsController(StoreContext context, IProductServices productServices, IMapper mapper)
        {
            _context = context;
            _productServices = productServices;
            _mapper = mapper;
        }

        // Post: api/Products/SearchPaging
        [HttpPost("search/paging")]
        public async Task<IActionResult> SearchProductsPaging(PagingResultVM<ProductDTO> pagingResultVM)
        {
            try
            {
                if (pagingResultVM == null)
                {
                    return BadRequest();
                }
                IEnumerable<ProductDTO> productResults = new List<ProductDTO>();
                var productEntities = await _productServices.GetMultiPaging(x =>
                x.Name.Contains(pagingResultVM.Keyword)
                || x.Description.Contains(pagingResultVM.Keyword),
                out int total,
                pagingResultVM.CurrentPage,
                pagingResultVM.PageSize,
                null).ToListAsync();

                productResults = _mapper.Map<IEnumerable<ProductDTO>>(productEntities);
                pagingResultVM.Results = productResults;
                pagingResultVM.TotalCount = total;
            }
            catch (Exception ex)
            {
                StatusCode(StatusCodes.Status500InternalServerError, new Response<ProductDTO> { Status = "Error", Message = ex.Message });
            }
            return Ok(new Response<ProductDTO> { Status = "200", Message = "SEARCH PRODUCT LIST SUCCESS", Results = pagingResultVM });
        }

        // GET: api/Products
        [HttpPost("getAll")]
        public async Task<IActionResult> GetProducts(PagingResultVM<ProductDTO> pagingResultVM)
        {
            IEnumerable<ProductDTO> productResults = new List<ProductDTO>();
            try
            {
                var productEntities = await _productServices.GetMultiPaging(null, out int total, pagingResultVM.CurrentPage, pagingResultVM.PageSize, null).ToListAsync();
                productResults = _mapper.Map<IEnumerable<ProductDTO>>(productEntities);
                pagingResultVM.Results = productResults;
                pagingResultVM.TotalCount = total;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<ProductDTO> { Status = "Error", Message = ex.Message });
            }

            return Ok(new Response<ProductDTO> { Results = pagingResultVM, Status = "200", Message = "GET LIST PRODUCT SUCCESS" });
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            ProductDTO productResult = null;
            try
            {
                var productEntity = await _productServices.GetProductByIdAsync(id);
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

        // GET: api/Products/Exports
        [HttpGet("Exports")]
        public async Task<IActionResult> ExportsProduct()
        {
            ProductDTO productResult = null;
            try
            {
                //var productEntity = await _productServices.GetProductByIdAsync(id);
                //productResult = _mapper.Map<ProductDTO>(productEntity);
                //if (productEntity == null)
                //{
                //    return Ok(new Response<ProductDTO> { Status = "200", Message = "NO RESULT" });
                //}
            }
            catch (Exception ex)
            {
                StatusCode(StatusCodes.Status500InternalServerError, new Response<ProductDTO> { Status = "Error", Message = ex.Message });
            }
            return Ok(new Response<ProductDTO> { Status = "200", Message = $"Export PRODUCT WITH  SUCCESS", Content = productResult });
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
                var imageProductEntites = propductEntity.Media;
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