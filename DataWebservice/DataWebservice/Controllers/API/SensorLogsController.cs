using System;
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
    public class SensorLogsController : ControllerBase
    {
        private readonly DataWebserviceContext _context;

        public SensorLogsController(DataWebserviceContext context)
        {
            _context = context;
        }

        // GET: api/SensorLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorLog>>> GetSensorLog()
        {
            return await _context.SensorLog.ToListAsync();
        }

        // GET: api/SensorLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorLog>> GetSensorLog(int id)
        {
            var sensorLog = await _context.SensorLog.FindAsync(id);

            if (sensorLog == null)
            {
                return NotFound();
            }

            return sensorLog;
        }

        // PUT: api/SensorLogs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensorLog(int id, SensorLog sensorLog)
        {
            if (id != sensorLog.sensorID)
            {
                return BadRequest();
            }

            _context.Entry(sensorLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorLogExists(id))
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

        // POST: api/SensorLogs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SensorLog>> PostSensorLog(SensorLog sensorLog)
        {
            _context.SensorLog.Add(sensorLog);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SensorLogExists(sensorLog.sensorID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSensorLog", new { id = sensorLog.sensorID }, sensorLog);
        }

        // DELETE: api/SensorLogs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SensorLog>> DeleteSensorLog(int id)
        {
            var sensorLog = await _context.SensorLog.FindAsync(id);
            if (sensorLog == null)
            {
                return NotFound();
            }

            _context.SensorLog.Remove(sensorLog);
            await _context.SaveChangesAsync();

            return sensorLog;
        }

        private bool SensorLogExists(int id)
        {
            return _context.SensorLog.Any(e => e.sensorID == id);
        }
    }
}
