using Address_Book.Core.Entities;
using Address_Book.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
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
        //Get All Departments
        //BaseUrl/api/Departments
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            var Jobs = await _unitOfWork.Repository<Department>().GetAllAsync();
            return Ok(Jobs);
        }
        //Create Department
        //BaseUrl/api/Departments
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Entry>> CreateDepartment([FromBody] Department department)
        {
            await _unitOfWork.Repository<Department>().Add(department);
            await _unitOfWork.CompleteAsync();

            return Ok(department);
        }

        //Delete Department
        //BaseUrl/api/Departments/id
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
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

        //Update
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateDepartment([FromBody] Department updatedDepartment)
        {
            try
            {
                await _unitOfWork.Repository<Department>().UpdateAsync(updatedDepartment);
                await _unitOfWork.CompleteAsync();
                return Ok(updatedDepartment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("search/{keyword}")]
        [Authorize]
        public async Task<IActionResult> SearchEntriesAsync(
         string keyword)
        {
            try
            {
                var Departments = await _unitOfWork.Repository<Department>().SearchEntriesAsync(keyword);
                return Ok(Departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
