using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductStore.RestfulAPI.Repositories.Infrastructure
{
    public interface IRepository<T> where T : class
    {// Marks an entity as new
        Task<T> Add(T entity);

        // Marks an entity as modified
        Task<T> Update(T entity);

        // Marks an entity to be removed
        Task<T> Delete(T entity);

        //Delete multi records
        Task DeleteMulti(Expression<Func<T, bool>> where);

        // Get an entity by int id
        Task<T> GetSingleById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="expression">GET BY EXPRESSION</param>
        /// <param name="includes">FILTER BY CONDITTIONS</param>
        /// <returns></returns>
        Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        Task<IQueryable<T>> GetAll(string[] includes = null);

        Task<IQueryable<T>> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        Task<IQueryable<T>> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

        Task<int> Count(Expression<Func<T, bool>> where);

        Task<bool> CheckContains(Expression<Func<T, bool>> predicate);
    }
}