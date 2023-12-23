using Address_Book.Core.Entities;
using Address_Book.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address_Book.Services
{
    public class EntryService
    {
        private readonly IGenericRepository<Entry> _entryRepository;
        public EntryService(IGenericRepository<Entry> entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<Entry> GetEntryByIdAsync(int id)
        {
            return await _entryRepository.GetByIdAsync(id, e => e.Department);
        }
    }
}
