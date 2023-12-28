using Address_Book.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Address_Book.Repository.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
