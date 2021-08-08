using Microsoft.EntityFrameworkCore;
using ProductStore.RestfulAPI.Data.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductStore.RestfulAPI.Repositories.Infrastructure
{

    /// <summary>
    /// INCLUDES/ JOIN => EXPRESSION/ FILTER => PAGINATION => RETURN RESULT
    /// </summary>
    /// <typeparam name="T"> ANY ENTITY IN DATABASE</typeparam>
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties

        private ProductStoreDbContext _dbContext;

        #endregion Properties
        protected RepositoryBase(ProductStoreDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        #region Implementation
        /// <summary>
        /// ADD AN ENTITY AND RECORD DATETIME, CREATE BY WITH IAuditEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<T> Add(T entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((IAuditEntity)entity).CreatedDate = DateTime.UtcNow;

            }
            _dbContext.AddAsync(entity);
            _dbContext.SaveChanges();

            return Task.FromResult(entity);
        }

        public Task<bool> CheckContains(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().AnyAsync(predicate);
        }

        public Task<int> Count(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression).CountAsync();
        }

        public Task<T> Delete(T entity)
        {
            if (typeof(IDeleteEntity).IsAssignableFrom(typeof(T)))
            {
                ((IDeleteEntity)entity).IsDeleted = true;
            }
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
            return Task.FromResult(entity);
        }

        public Task DeleteMulti(Expression<Func<T, bool>> expression)
        {
            IEnumerable<T> objects = _dbContext.Set<T>().Where<T>(expression).AsEnumerable();
            foreach (T obj in objects)
            {
                if (typeof(IDeleteEntity).IsAssignableFrom(typeof(T)))
                {
                    ((IDeleteEntity)obj).IsDeleted = true;
                }
                _dbContext.Remove(obj);
            }
            return Task.FromResult("true");
        }

        public Task<IQueryable<T>> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include<T>(include);
                return Task.FromResult(query.AsQueryable());
            }
            return Task.FromResult(_dbContext.Set<T>().AsQueryable());
        }

        public virtual Task<IQueryable<T>> GetMany(Expression<Func<T, bool>> where, string includes)
        {
            return Task.FromResult(_dbContext.Set<T>().Where(where).AsQueryable());
        }

        public Task<IQueryable<T>> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbContext.Set<T>().Include<T>(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return Task.FromResult(query.Where<T>(predicate).AsQueryable<T>());
            }

            return Task.FromResult(_dbContext.Set<T>().Where<T>(predicate).AsQueryable<T>());
        }

        public Task<IQueryable<T>> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50, string[] includes = null)
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
            return Task.FromResult(_resetSet.AsQueryable());
        }

        public Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            T item;
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);

                item = expression != null ? query.Where<T>(expression).AsQueryable().FirstOrDefault() : query.AsQueryable().FirstOrDefault();
                return Task.FromResult(item);

            }
            item = expression != null ? _dbContext.Set<T>().Where<T>(expression).AsQueryable().FirstOrDefault() : _dbContext.Set<T>().AsQueryable().FirstOrDefault();
            return Task.FromResult(item);  
        }

        public Task<T> GetSingleById(int id)
        {
            return Task.FromResult(_dbContext.Set<T>().Find(id));
        }

        public Task<T> Update(T entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((IAuditEntity)entity).CreatedDate = DateTime.Now;
            }
            _dbContext.Attach(entity);
            _dbContext.Update(entity);
            return Task.FromResult(entity);
        }
        #endregion Implementation
    }
}