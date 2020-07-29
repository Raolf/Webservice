using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataWebservice.Data;
using DataWebservice.Models;
using DataWebservice.Models.apiDTOs;

namespace DataWebservice.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly DataWebserviceContext _context;

        public DataController(DataWebserviceContext context)
        {
            _context = context;
        }

        // GET: api/Data
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataDTO>>> GetData()
        {
            var dataList = await _context.Data.ToListAsync();
            var DTOList = new List<DataDTO>();
            foreach (Models.Data data in dataList)
            {
                DTOList.Add(data.DataToDTO());
            }

            return DTOList;
        }

        // GET: api/Data/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataDTO>> GetData(int id)
        {
            var data = await _context.Data.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return data.DataToDTO();
        }

        // PUT: api/Data/5
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

        // POST: api/Data
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

        // DELETE: api/Data/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Data>> DeleteData(int id, DateTime dateTime)
        {
            var data = await _context.Data.FindAsync(id, dateTime);
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
