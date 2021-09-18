using Microsoft.EntityFrameworkCore;
using ProductStore.API.DBFirst.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.Services.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties

        protected StoreContext _dbContext { get; set; }

        protected RepositoryBase(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion Properties

        #region Implementation

        public void Add(T entity)
        {
           _dbContext.Add(entity);

        }

        public void AddMulti(List<T> entity)
        {
            _dbContext.AddRange(entity);

        }

        public bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Count<T>(predicate) > 0;
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            return _dbContext.Set<T>().Count(where);
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string includes)
        {
            return _dbContext.Set<T>().Where(where).ToList();
        }

        public IQueryable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<T>(predicate).AsQueryable<T>();
            }

            return _dbContext.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }

        public IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? _dbContext.Set<T>().Where<T>(predicate).AsQueryable() : _dbContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _dbContext.Products.Count();
            return _resetSet.AsQueryable();
        }

        public IQueryable<T> SearchPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? _dbContext.Set<T>().Where<T>(predicate).AsQueryable() : _dbContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public async Task<T> GetSingleById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        //public IQueryable<T> GetSingleByCondition(Expression<Func<T, bool>> expression)
        //{
        //    return _dbContext.Set<T>().Where(expression).AsNoTracking();
        //}

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>()
                .Where(expression).AsQueryable<T>();
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            var query = _dbContext.Set<T>().Include(includes.First());
            foreach (var include in includes.Skip(1))
                query = query.Include(include);

            return  _dbContext.Set<T>().Where(expression).AsQueryable<T>().FirstOrDefault();
        }

        public IQueryable<T> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.AsNoTracking();
            }

            return _dbContext.Set<T>().AsNoTracking();
        }

        public void UpdateMultiById(IEnumerable<T> listT)
        {
            _dbContext.Set<T>().UpdateRange(listT);
        }

        public void DeleteMulti(Expression<Func<T, bool>> expression, IEnumerable<T> listT)
        {
            var listToRemove=_dbContext.Set<T>().Where(expression).AsQueryable(); 
            _dbContext.RemoveRange(listToRemove);
        }


        #endregion Implementation
    }
}