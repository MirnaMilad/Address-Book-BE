using Address_Book.Core.Entities;
using Address_Book.Core.Repositories;
using Address_Book.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Address_Book.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : baseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Entry)){
                return (IEnumerable<T>) await _dbContext.Entries.Include(E=>E.Department).Include(E=>E.Job).ToListAsync();
            }
            else
            {
            return await _dbContext.Set<T>().ToListAsync(); 
            }
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            if (typeof(T) == typeof(Entry))
            {
                IQueryable<T> query = _dbContext.Set<T>();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                    IQueryable<Entry> entryQuery = (IQueryable<Entry>)query;
                    entryQuery = entryQuery.Include(e => e.Department).Include(e => e.Job);
                    return (T)(object)await entryQuery.FirstOrDefaultAsync(e => e.Id == id);
            }
            else
            {
                return await _dbContext.Set<T>().FindAsync(id);
            }
        }

        public async Task Add(T item)
        {

            await _dbContext.Set<T>().AddAsync(item);
            

        }

        public async Task Delete(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new InvalidOperationException($"Entity with ID {id} not found.");
            }
            
            _dbContext.Set<T>().Remove(entity);
        }

        public void Update(T item)
        {
            _dbContext.Set<T>().Update(item);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<IEnumerable<T>> GetAllAsyncWithSpecifications(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes)
        {

            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }


    }
}
