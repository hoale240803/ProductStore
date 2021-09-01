using System;
using System.Linq;

namespace ProductStore.API.DBFirst.ViewModels.PagedResult
{
    //public class PagedResultVM
    //{
    //    public  PagedResultVM<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
    //    {
    //        var result = new PagedResultVM<T>();
    //        result.CurrentPage = page;
    //        result.PageSize = pageSize;
    //        result.RowCount = query.Count();

    //        var pageCount = (double)result.RowCount / pageSize;
    //        result.PageCount = (int)Math.Ceiling(pageCount);

    //        var skip = (page - 1) * pageSize;
    //        result.Results = query.Skip(skip).Take(pageSize).ToList();

    //        return result;
    //    }
    //}
}