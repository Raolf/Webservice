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
    public class DataController : Controller
    {
        private readonly DataWebserviceContext _context;

        public DataController(DataWebserviceContext context)
        {
            _context = context;
        }

        // GET: Data
        public async Task<IActionResult> Index()
        {
            var dataWebserviceContext = _context.Data.Include(d => d.sensor);
            return View(await dataWebserviceContext.ToListAsync());
        }

        // GET: Data/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data = await _context.Data
                .Include(d => d.sensor)
                .FirstOrDefaultAsync(m => m.sensorID == id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        // GET: Data/Create
        public IActionResult Create()
        {
            ViewData["sensorID"] = new SelectList(_context.Set<Sensor>(), "sensorID", "sensorID");
            return View();
        }

        // POST: Data/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("sensorID,timestamp,humidity,CO2,temperature")] Models.Data data)
        {
            if (ModelState.IsValid)
            {
                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["sensorID"] = new SelectList(_context.Set<Sensor>(), "sensorID", "sensorID", data.sensorID);
            return View(data);
        }

        // GET: Data/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data = await _context.Data.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            ViewData["sensorID"] = new SelectList(_context.Set<Sensor>(), "sensorID", "sensorID", data.sensorID);
            return View(data);
        }

        // POST: Data/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("sensorID,timestamp,humidity,CO2,temperature")] Models.Data data)
        {
            if (id != data.sensorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataExists(data.sensorID))
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
            ViewData["sensorID"] = new SelectList(_context.Set<Sensor>(), "sensorID", "sensorID", data.sensorID);
            return View(data);
        }

        // GET: Data/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data = await _context.Data
                .Include(d => d.sensor)
                .FirstOrDefaultAsync(m => m.sensorID == id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        // POST: Data/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await _context.Data.FindAsync(id);
            _context.Data.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DataExists(int id)
        {
            return _context.Data.Any(e => e.sensorID == id);
        }
    }
}
