using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductStore.API.DBFirst.Services.Infrastructure
{
    public interface IRepository<T> where T : class
    {// Marks an entity as new
        void Add(T entity);

        void AddMulti(List<T> entity);

        // Marks an entity as modified
        void Update(T entity);

        void UpdateMultiById(IEnumerable<T> listT);

        // Marks an entity to be removed
        void Delete(T entity);

        //Delete multi records
        void DeleteMulti(Expression<Func<T, bool>> expression, IEnumerable<T> listT);

        // Get an entity by int id
        Task<T> GetSingleById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="expression">GET BY EXPRESSION</param>
        /// <param name="includes">FILTER BY CONDITTIONS</param>
        /// <returns></returns>
        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        IQueryable<T> GetAll(string[] includes = null);

        IQueryable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);

    }
}