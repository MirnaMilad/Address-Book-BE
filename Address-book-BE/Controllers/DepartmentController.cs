using Address_Book.Core.Entities;
using Address_Book.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Address_book_BE.Controllers
{
    public class DepartmentsController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //Get All Entries
        //BaseUrl/api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetEntries()
        {
            var Jobs = await _unitOfWork.Repository<Department>().GetAllAsync();
            return Ok(Jobs);
        }
        //Create Department
        //BaseUrl/api/Departments
        [HttpPost]
        public async Task<ActionResult<Entry>> CreateJob([FromBody] Department department)
        {
            await _unitOfWork.Repository<Department>().Add(department);
            await _unitOfWork.CompleteAsync();

            return Ok(department);
        }

        //Delete Department
        //BaseUrl/api/Departments/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteJob(int id)
        {
            try
            {
                await _unitOfWork.Repository<Department>().Delete(id);
                var relatedEntries = await _unitOfWork.Repository<Entry>().GetAllAsyncWithSpecifications(e => e.DepartmentId == id);

                foreach (var entry in relatedEntries)
                {
                    entry.DepartmentId = null;
                }
                await _unitOfWork.CompleteAsync();
                return Ok("Successfully removed");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
