using Address_Book.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Address_Book.Core.Repositories
{
    public interface IGenericRepository<T>where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(); 
        Task<IEnumerable<T>> GetAllAsyncWithSpecifications(Expression<Func<T, bool>>? filter = null,
                                     Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                     params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task Add(T item);

        Task Delete(int id);
        Task UpdateAsync(T entity);
        Task<IEnumerable<T>> SearchEntriesAsync(string keyword);

    }
}
