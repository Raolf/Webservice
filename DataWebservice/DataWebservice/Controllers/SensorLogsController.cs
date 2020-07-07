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
    public class SensorLogsController : Controller
    {
        private readonly DataWebserviceContext _context;

        public SensorLogsController(DataWebserviceContext context)
        {
            _context = context;
        }

        // GET: SensorLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.SensorLog.ToListAsync());
        }

        // GET: SensorLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensorLog = await _context.SensorLog
                .FirstOrDefaultAsync(m => m.sensorID == id);
            if (sensorLog == null)
            {
                return NotFound();
            }

            return View(sensorLog);
        }

        // GET: SensorLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SensorLogs/Create
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
            return View(sensorLog);
        }

        // GET: SensorLogs/Edit/5
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
            return View(sensorLog);
        }

        // POST: SensorLogs/Edit/5
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
            return View(sensorLog);
        }

        // GET: SensorLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensorLog = await _context.SensorLog
                .FirstOrDefaultAsync(m => m.sensorID == id);
            if (sensorLog == null)
            {
                return NotFound();
            }

            return View(sensorLog);
        }

        // POST: SensorLogs/Delete/5
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
