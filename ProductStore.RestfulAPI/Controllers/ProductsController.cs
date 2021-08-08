using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductStore.RestfulAPI.Data;
using ProductStore.RestfulAPI.Data.Products;
using ProductStore.RestfulAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductStore.RestfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository;
        private IMapper _mapper;
        private IProductCategoryRepository _productCategoryRepository;
        private IProductTagsRepository _productTagsRepository;

        public ProductsController(IMapper mapper, IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IProductTagsRepository productTagsRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productTagsRepository = productTagsRepository;
        }

        // GET: api/Products
        [HttpGet("getallpaging")]
        public IEnumerable<ProductsEntity> GetProducts(int pageSize, int startIndex)
        {
            return _productRepository.GetAllByPaging(pageSize, startIndex);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsEntity>> GetProducts(int id)
        {
            var products = await _productRepository.GetSingleById(id);
            if (products == null)
            {
                return NoContent();
            }

            return Ok(products);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutProducts(int id, ProductsEntity products)
        {
            if (id != products.Id)
            {
                return NotFound(" id not found");
            }

            //_productRepository.Update(products);

            return Ok("oke");
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<ProductsEntity> PostProducts(ProductsEntity productFull)
        {
            //var result=_productRepository.Add(products);
            // add product table
            //var prodCategory = _mapper.Map<ProductCategoryEntity>(productFull.ProductCategory);
            //_productCategoryRepository.Add(prodCategory);
            //add prodcut_category table
            var productEntity = _mapper.Map<ProductsEntity>(productFull);
            _productRepository.Add(productEntity);
            //// add list image
            //var prodImage = _mapper.Map<ProductImageEntity>(productFull.ThumbnailImage);

            //_pro

            return Ok(productFull);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProducts(ProductsEntity Product)
        {
            if (Product == null)
            {
                return null;
            }
            var product = _productRepository.Delete(Product);

            return Ok("delete success");
        }
    }
}