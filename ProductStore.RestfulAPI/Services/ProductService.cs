//using ProductStore.RestfulAPI.Data;
//using ProductStore.RestfulAPI.Repositories;
//using ProductStore.RestfulAPI.Repositories.Infrastructure;
//using System.Collections.Generic;

//namespace ProductStore.RestfulAPI.Services
//{
//    public interface IProductService
//    {
//        Products Add(Products Product);

//        void Update(Products Product);

//        Products Delete(Products Product);

//        IEnumerable<Products> GetAll();

//        IEnumerable<Products> GetAllByPaging(int pageSize, int startIndex);

//        IEnumerable<Products> GetAll(string keyword);

//        IEnumerable<Products> GetLastest(int top);

//        IEnumerable<Products> GetHotProduct(int top);

//        IEnumerable<Products> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow);

//        IEnumerable<Products> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

//        IEnumerable<Products> GetListProduct(string keyword);

//        IEnumerable<Products> GetReatedProducts(int id, int top);

//        IEnumerable<string> GetListProductByName(string name);

//        Products GetById(int id);

//        void Save();

//        IEnumerable<Tags> GetListTagByProductId(int id);

//        Tags GetTag(string tagId);

//        void IncreaseView(int id);

//        IEnumerable<Products> GetListProductByTag(string tagId, int page, int pagesize, out int totalRow);

//        bool SellProduct(int productId, int quantity);
//    }

//    public class ProductService : IProductService
//    {
//        private IProductRepository _productRepository;
//        private IProductTagsRepository _productTagsRepository;
//        private IUnitOfWork _unitOfWork;

//        public ProductService(IProductRepository productsRepository, IProductTagsRepository productTagsRepository, IUnitOfWork unitOfWork)
//        {
//            _productRepository = productsRepository;
//            _productTagsRepository = productTagsRepository;
//            _unitOfWork = unitOfWork;
//        }

//        public Products Add(Products Product)
//        {
//            return _productRepository.Add(Product);
//        }

//        public Products Delete(Products Product)
//        {
//            return _productRepository.Delete(Product);
//        }

//        public IEnumerable<Products> GetAll()
//        {
//            throw new System.NotImplementedException();
//        }

//        public IEnumerable<Products> GetAll(string keyword)
//        {
//            throw new System.NotImplementedException();
//        }

//        public IEnumerable<Products> GetAllByPaging(int pageSize, int startIndex)
//        {
//            return _productRepository.GetAllByPaging(pageSize, startIndex);
//        }

//        public Products GetById(int id)
//        {
//            return _productRepository.GetSingleById(id);
//        }

//        public IEnumerable<Products> GetHotProduct(int top)
//        {
//            throw new System.NotImplementedException();
//        }

//        public IEnumerable<Products> GetLastest(int top)
//        {
//            throw new System.NotImplementedException();
//        }

//        public IEnumerable<Products> GetListProduct(string keyword)
//        {
//            throw new System.NotImplementedException();
//        }

//        public IEnumerable<Products> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow)
//        {
//            throw new System.NotImplementedException();
//        }

//        public IEnumerable<string> GetListProductByName(string name)
//        {
//            throw new System.NotImplementedException();
//        }

//        public IEnumerable<Products> GetListProductByTag(string tagId, int page, int pagesize, out int totalRow)
//        {
//            throw new System.NotImplementedException();
//        }

//        public IEnumerable<Tags> GetListTagByProductId(int id)
//        {
//            throw new System.NotImplementedException();
//        }

//        public IEnumerable<Products> GetReatedProducts(int id, int top)
//        {
//            throw new System.NotImplementedException();
//        }

//        public Tags GetTag(string tagId)
//        {
//            throw new System.NotImplementedException();
//        }

//        public void IncreaseView(int id)
//        {
//            throw new System.NotImplementedException();
//        }

//        public void Save()
//        {
//            throw new System.NotImplementedException();
//        }

//        public IEnumerable<Products> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
//        {
//            throw new System.NotImplementedException();
//        }

//        public bool SellProduct(int productId, int quantity)
//        {
//            throw new System.NotImplementedException();
//        }

//        public void Update(Products Product)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}