using Microsoft.EntityFrameworkCore;
using ProductStore.API.DBFirst.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.Services.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        #region Properties

        protected StoreContext DbContext { get; set; }

        protected RepositoryBase(StoreContext dbContext)
        {
            this.DbContext = dbContext;
        }

        #endregion Properties

        #region Implementation

        public void Add(T entity)
        {
            this.DbContext.Add(entity);
        }

        public void AddMulti(List<T> entity)
        {
            this.DbContext.AddRange(entity);
        }

        public bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return this.DbContext.Set<T>().Count<T>(predicate) > 0;
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            return this.DbContext.Set<T>().Count(where);
        }

        public void Delete(T entity)
        {
            this.DbContext.Remove(entity);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string includes)
        {
            return this.DbContext.Set<T>().Where(where).ToList();
        }

        public IQueryable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = this.DbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<T>(predicate).AsQueryable<T>();
            }

            return this.DbContext.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }

        public IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = this.DbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? this.DbContext.Set<T>().Where<T>(predicate).AsQueryable() : this.DbContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = this.DbContext.Set<T>().Count();
            return _resetSet.AsQueryable();
        }

        public IQueryable<T> SearchPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = this.DbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? this.DbContext.Set<T>().Where<T>(predicate).AsQueryable() : this.DbContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public async Task<T> GetSingleById(int id)
        {
            return await this.DbContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            this.DbContext.Set<T>().Attach(entity);
            this.DbContext.Entry(entity).State = EntityState.Modified;
        }

        //public IQueryable<T> GetSingleByCondition(Expression<Func<T, bool>> expression)
        //{
        //    return _dbContext.Set<T>().Where(expression).AsNoTracking();
        //}

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.DbContext.Set<T>()
                .Where(expression).AsQueryable<T>();
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            var query = this.DbContext.Set<T>().Include(includes.First());
            foreach (var include in includes.Skip(1))
                query = query.Include(include);

            return this.DbContext.Set<T>().Where(expression).AsQueryable<T>().FirstOrDefault();
        }

        public IQueryable<T> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = this.DbContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.AsNoTracking();
            }

            return this.DbContext.Set<T>().AsNoTracking();
        }

        public void UpdateMultiById(IEnumerable<T> listT)
        {
            this.DbContext.Set<T>().UpdateRange(listT);
        }

        public void DeleteMulti(Expression<Func<T, bool>> expression, IEnumerable<T> listT)
        {
            var listToRemove = this.DbContext.Set<T>().Where(expression).AsQueryable();
            this.DbContext.RemoveRange(listToRemove);
        }

        #endregion Implementation
    }
}