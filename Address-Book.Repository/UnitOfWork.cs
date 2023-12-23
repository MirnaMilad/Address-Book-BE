using Address_Book.Core.Entities;
using Address_Book.Core.Repositories;
using Address_Book.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address_Book.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly StoreContext _dbContext;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync() =>
        await _dbContext.DisposeAsync();


        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : baseEntity
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                //Create
                var Repository = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(type, Repository);

            }
            return _repositories[type] as IGenericRepository<TEntity>;

        }
    }
}

