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
    public class RoomAccessesController : Controller
    {
        private readonly DataWebserviceContext _context;

        public RoomAccessesController(DataWebserviceContext context)
        {
            _context = context;
        }

        // GET: RoomAccesses
        public async Task<IActionResult> Index()
        {
            var dataWebserviceContext = _context.RoomAccess.Include(r => r.room).Include(r => r.user);
            return View(await dataWebserviceContext.ToListAsync());
        }

        // GET: RoomAccesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomAccess = await _context.RoomAccess
                .Include(r => r.room)
                .Include(r => r.user)
                .FirstOrDefaultAsync(m => m.roomID == id);
            if (roomAccess == null)
            {
                return NotFound();
            }

            return View(roomAccess);
        }

        // GET: RoomAccesses/Create
        public IActionResult Create()
        {
            ViewData["roomID"] = new SelectList(_context.Room, "roomID", "roomID");
            ViewData["userID"] = new SelectList(_context.Set<User>(), "userID", "userID");
            return View();
        }

        // POST: RoomAccesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("userID,roomID")] RoomAccess roomAccess)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomAccess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["roomID"] = new SelectList(_context.Room, "roomID", "roomID", roomAccess.roomID);
            ViewData["userID"] = new SelectList(_context.Set<User>(), "userID", "userID", roomAccess.userID);
            return View(roomAccess);
        }

        // GET: RoomAccesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomAccess = await _context.RoomAccess.FindAsync(id);
            if (roomAccess == null)
            {
                return NotFound();
            }
            ViewData["roomID"] = new SelectList(_context.Room, "roomID", "roomID", roomAccess.roomID);
            ViewData["userID"] = new SelectList(_context.Set<User>(), "userID", "userID", roomAccess.userID);
            return View(roomAccess);
        }

        // POST: RoomAccesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("userID,roomID")] RoomAccess roomAccess)
        {
            if (id != roomAccess.roomID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomAccess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomAccessExists(roomAccess.roomID))
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
            ViewData["roomID"] = new SelectList(_context.Room, "roomID", "roomID", roomAccess.roomID);
            ViewData["userID"] = new SelectList(_context.Set<User>(), "userID", "userID", roomAccess.userID);
            return View(roomAccess);
        }

        // GET: RoomAccesses/Delete/5
        public async Task<IActionResult> Delete(int? id, int userID)
        {

            var roomAccess = await _context.RoomAccess.Where(ra => ra.roomID == id && ra.userID == userID).Include(r => r.room).Include(r => r.user).FirstAsync();


            if (id == null)
            {
                return NotFound();
            }

            roomAccess = await _context.RoomAccess
                .Include(r => r.room)
                .Include(r => r.user)
            .FirstOrDefaultAsync(m => m.roomID == id);
            if (roomAccess == null)
            {
                return NotFound();
            }

            return View(roomAccess);
        }

        // POST: RoomAccesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int userID)
        {
            var roomAccess = await _context.RoomAccess.Where(ra => ra.roomID == id && ra.userID == userID).FirstAsync();

            _context.RoomAccess.Remove(roomAccess);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomAccessExists(int id)
        {
            return _context.RoomAccess.Any(e => e.roomID == id);
        }
    }
}
