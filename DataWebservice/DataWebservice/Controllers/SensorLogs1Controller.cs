using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataWebservice.Data;
using DataWebservice.Models;

namespace DataWebservice.Controllers
{
    public class SensorLogs1Controller : Controller
    {
        private readonly DataWebserviceContext _context;

        public SensorLogs1Controller(DataWebserviceContext context)
        {
            _context = context;
        }

        // GET: SensorLogs1
        public async Task<IActionResult> Index()
        {
            var dataWebserviceContext = _context.SensorLog.Include(s => s.sensor);
            return View(await dataWebserviceContext.ToListAsync());
        }

        // GET: SensorLogs1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensorLog = await _context.SensorLog
                .Include(s => s.sensor)
                .FirstOrDefaultAsync(m => m.sensorID == id);
            if (sensorLog == null)
            {
                return NotFound();
            }

            return View(sensorLog);
        }

        // GET: SensorLogs1/Create
        public IActionResult Create()
        {
            ViewData["sensorID"] = new SelectList(_context.Sensor, "sensorID", "sensorID");
            return View();
        }

        // POST: SensorLogs1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("sensorID,timestamp,servoSetting")] SensorLog sensorLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sensorLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["sensorID"] = new SelectList(_context.Sensor, "sensorID", "sensorID", sensorLog.sensorID);
            return View(sensorLog);
        }

        // GET: SensorLogs1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensorLog = await _context.SensorLog.FindAsync(id);
            if (sensorLog == null)
            {
                return NotFound();
            }
            ViewData["sensorID"] = new SelectList(_context.Sensor, "sensorID", "sensorID", sensorLog.sensorID);
            return View(sensorLog);
        }

        // POST: SensorLogs1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("sensorID,timestamp,servoSetting")] SensorLog sensorLog)
        {
            if (id != sensorLog.sensorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sensorLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SensorLogExists(sensorLog.sensorID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["sensorID"] = new SelectList(_context.Sensor, "sensorID", "sensorID", sensorLog.sensorID);
            return View(sensorLog);
        }

        // GET: SensorLogs1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensorLog = await _context.SensorLog
                .Include(s => s.sensor)
                .FirstOrDefaultAsync(m => m.sensorID == id);
            if (sensorLog == null)
            {
                return NotFound();
            }

            return View(sensorLog);
        }

        // POST: SensorLogs1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sensorLog = await _context.SensorLog.FindAsync(id);
            _context.SensorLog.Remove(sensorLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SensorLogExists(int id)
        {
            return _context.SensorLog.Any(e => e.sensorID == id);
        }
    }
}
