//https://github.com/Zungate/SEP4DW
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataWebservice.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace DataWebservice.Controllers
{
    //[Authorize(Policy = "Administrator")]
    public class RoleController : Controller
    {
        private readonly DataWebserviceContext _context;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<IdentityUser> _userManager;

        public RoleController(DataWebserviceContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Role
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }

        // GET: Role/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "Error");
            }

            var role = await _context.Roles.FirstOrDefaultAsync(m => m.Id == id);

            IdentityUser user1 = new IdentityUser("bob");

            var userroles = await _userManager.GetUsersInRoleAsync(role.Name);
            var users = new List<IdentityUser>();

            foreach (var user in userroles)
            {
                users.Add(user);
            }

            if (role == null)
            {
                return RedirectToAction("Error404", "Error");
            }

            ViewData["Users"] = users;
            return View(role);
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] IdentityRole role)
        {
            IdentityResult roleResult;
            if (ModelState.IsValid)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role.Name);

                if (!roleExist)
                {
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(role.Name));
                    TempData["Success"] = $"{role.Name} has been created";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "A Room with that name already exist";
                    return RedirectToAction("Create");
                }
            }
            return View(role);
        }

        // GET: Role/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "Error");
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return RedirectToAction("Error404", "Error");
            }
            return View(role);
        }

        // POST: Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name")] IdentityRole role)
        {
            if (id != role.Id)
            {
                return RedirectToAction("Error404", "Error");
            }
            if (_context.Roles.Any(r => r.Id != role.Id && r.Name == role.Name))
            {
                TempData["Error"] = "A role with that name already exist";
                return View();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var existingRole = await _roleManager.FindByIdAsync(role.Id);
                    existingRole.Name = role.Name;
                    var result = await _roleManager.UpdateAsync(existingRole);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First().ToString());
                        return View();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                TempData["Success"] = $"{role.Name} has been edited";
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Role/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "Error");
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return RedirectToAction("Error404", "Error");
            }

            return View(role);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _context.Roles.FindAsync(id);
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            TempData["Success"] = $"{role.Name} has been deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(string id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
