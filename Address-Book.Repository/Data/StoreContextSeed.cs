using Address_Book.Core.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Threading.Tasks;

namespace Address_Book.Repository.Data
{
    public class StoreContextSeed
    {
        // Seeding
        public static async Task SeedAsync(StoreContext dbContext)
        {
            if (!dbContext.Job.Any())
            {
                try
                {
                    var filePath = "../Address-Book.Repository/Data/DataSeed/Jobs.json";
                    var JobsData = File.ReadAllText(filePath);

                    var Jobs = JsonSerializer.Deserialize<List<Job>>(JobsData);

                    if (Jobs?.Count > 0)
                    {
                        foreach (var job in Jobs)
                        {
                            await dbContext.Set<Job>().AddAsync(job);
                        }

                        await dbContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during seeding: {ex.Message}");
                }
                
            }if (!dbContext.Departments.Any())
            {
                try
                {
                    var filePath = "../Address-Book.Repository/Data/DataSeed/Departments.json";
                    var DepartmentsData = File.ReadAllText(filePath);

                    var Departments = JsonSerializer.Deserialize<List<Department>>(DepartmentsData);

                    if (Departments?.Count > 0)
                    {
                        foreach (var department in Departments)
                        {
                            await dbContext.Set<Department>().AddAsync(department);
                        }

                        await dbContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during seeding: {ex.Message}");
                }
                
            }
           
        }
    }
}