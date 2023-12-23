﻿using Address_Book.Core.Entities;
using Address_Book.Core.Repositories;
using Address_Book.Repository;
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
        //Get All Entries
        //BaseUrl/api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetEntries()
        {
            var Jobs = await _unitOfWork.Repository<Job>().GetAllAsync();
            return Ok(Jobs);
        }

        //Create Job
        //BaseUrl/api/Jobs
        [HttpPost]
        public async Task<ActionResult<Entry>> CreateJob([FromBody] Job job)
        {
            await _unitOfWork.Repository<Job>().Add(job);
            await _unitOfWork.CompleteAsync();

            return Ok(job);
        }

        //Delete Job
        //BaseUrl/api/Jobs/id
        [HttpDelete("{id}")]
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
    }
}