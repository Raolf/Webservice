﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataWebservice.Data;
using DataWebservice.Models;

namespace DataWebservice.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataOriginalController : ControllerBase
    {
        private readonly DataWebserviceContext _context;

        public DataOriginalController(DataWebserviceContext context)
        {
            _context = context;
        }

        // GET: api/DataOriginal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Data>>> GetData()
        {
            return await _context.Data.ToListAsync();
        }

        // GET: api/DataOriginal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Data>> GetData(int id)
        {
            var data = await _context.Data.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return data;
        }

        // PUT: api/DataOriginal/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutData(int id, Models.Data data)
        {
            if (id != data.sensorID)
            {
                return BadRequest();
            }

            _context.Entry(data).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DataOriginal
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Models.Data>> PostData(Models.Data data)
        {
            _context.Data.Add(data);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DataExists(data.sensorID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetData", new { id = data.sensorID }, data);
        }

        // DELETE: api/DataOriginal/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Data>> DeleteData(int id)
        {
            var data = await _context.Data.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            _context.Data.Remove(data);
            await _context.SaveChangesAsync();

            return data;
        }

        private bool DataExists(int id)
        {
            return _context.Data.Any(e => e.sensorID == id);
        }
    }
}
