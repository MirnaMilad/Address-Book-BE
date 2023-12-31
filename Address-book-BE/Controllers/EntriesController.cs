﻿using Address_Book.Core.Entities;
using Address_Book.Core.Repositories;
using Address_Book.Repository;
using Address_Book.Repository.Data;
using Address_book_BE.Dtos;
using Address_book_BE.Errors;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;

namespace Address_book_BE.Controllers
{
    public class EntriesController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EntriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //Get All Entries
        //BaseUrl/api/Entries
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Entry>>> GetEntries()
        {
            try
            {
                var Entries = await _unitOfWork.Repository<Entry>().GetAllAsync();
                var MappedEntries = _mapper.Map<IEnumerable<Entry>, IEnumerable<EntryToReturnDto>>(Entries);
                return Ok(MappedEntries);
            } catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //Get Entry By Id
        //BaseUrl/api/Entries/Id
        [HttpGet("{Id}")]
        [Authorize]
        public async Task<ActionResult<Entry>> GetEntryById(int id)
        {
            var Entry = await _unitOfWork.Repository<Entry>().GetByIdAsync(id);
            var MappedEntry = _mapper.Map<Entry, EntryToReturnDto>(Entry);
            return Ok(MappedEntry);
        }

        //Create Entry
        //BaseUrl/api/Entries
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Entry>> CreateEntry([FromBody] Entry entry)
        {
            await _unitOfWork.Repository<Entry>().Add(entry);
            await _unitOfWork.CompleteAsync();

            return Ok(entry);
        }

        //Delete Entry
        //BaseUrl/api/Entries/id
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Entry>> DeleteEntry(int id)
        {
            try
            {
                await _unitOfWork.Repository<Entry>().Delete(id);
                await _unitOfWork.CompleteAsync();
                return Ok( StatusCode(200));
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            

            
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateEntry([FromBody] Entry updatedEntry)
        {
            try
            {
                await _unitOfWork.Repository<Entry>().UpdateAsync(updatedEntry);
                await _unitOfWork.CompleteAsync();
                return Ok(updatedEntry);
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
                var entries = await _unitOfWork.Repository<Entry>().SearchEntriesAsync(keyword);
                return Ok(entries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
