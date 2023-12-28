using Address_Book.Core.Entities;
using Address_Book.Core.Repositories;
using Address_Book.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Address_book_BE.Controllers
{
    public class JobsController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //Get All Jobs
        //BaseUrl/api/Jobs
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Job>>> GetEntries()
        {
            var Jobs = await _unitOfWork.Repository<Job>().GetAllAsync();
            return Ok(Jobs);
        }

        //Create Job
        //BaseUrl/api/Jobs
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Entry>> CreateJob([FromBody] Job job)
        {
            await _unitOfWork.Repository<Job>().Add(job);
            await _unitOfWork.CompleteAsync();

            return Ok(job);
        }

        //Delete Job
        //BaseUrl/api/Jobs/id
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Job>> DeleteJob(int id)
        {
            try
            {
                await _unitOfWork.Repository<Job>().Delete(id);
                var relatedEntries = await _unitOfWork.Repository<Entry>().GetAllAsyncWithSpecifications(e => e.JobId == id);

                foreach (var entry in relatedEntries)
                {
                    entry.JobId = null;
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
        public async Task<IActionResult> UpdateDepartment([FromBody] Job updatedJob)
        {
            try
            {
                await _unitOfWork.Repository<Job>().UpdateAsync(updatedJob);
                await _unitOfWork.CompleteAsync();
                return Ok(updatedJob);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        //Search
        [HttpGet("search/{keyword}")]
        [Authorize]
        public async Task<IActionResult> SearchEntriesAsync(
         string keyword)
        {
            try
            {
                var Jobs = await _unitOfWork.Repository<Job>().SearchEntriesAsync(keyword);
                return Ok(Jobs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
