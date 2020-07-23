using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataWebservice.Data;
using DataWebservice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace DataWebservice.Controllers
{
    //[Authorize(Policy = "Administrator")]
    public class UserRoleController : Controller
    {
        private readonly DataWebserviceContext _context;
        RoleManager<IdentityRole> _roleManager;
        UserManager<IdentityUser> _userManager;

        public UserRoleController(DataWebserviceContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // POST: UserRole/AddRoleToUser/5
        // Add role to user with id
        public async Task<IActionResult> RoleManagement(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "Error");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return RedirectToAction("Error404", "Error");
            }
            var viewModel = new UserRoleViewModel
            {
                UserID = user.Id

            };

            var userRoles = await _userManager.GetRolesAsync(user);
            List<IdentityRole> roles = new List<IdentityRole>();

            foreach (var role in userRoles)
            {
                roles.Add(await _roleManager.FindByNameAsync(role));
            }


            ViewData["User"] = user;
            ViewData["Roles"] = roles;
            ViewData["RoleID"] = new SelectList(_context.Roles, "Id", "Name");
            return View(viewModel);
        }


        // POST: UserRole/AddRoleToUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleToUser([Bind("UserID,RoleID")] string userID, string roleID)
        {
            if (userID == null || roleID == null)
            {
                return RedirectToAction("Error404", "Error");
            }

            var user = await _context.Users.FindAsync(userID);
            var role = await _context.Roles.FindAsync(roleID);
            if (user == null || role == null)
            {
                return RedirectToAction("Error404", "Error");
            }

            await _userManager.AddToRoleAsync(user, role.Name);
            TempData["Success"] = $"{role.Name} has been added to {user.UserName}";


            ViewData["User"] = user;
            ViewData["Roles"] = await _userManager.GetRolesAsync(user);
            ViewData["RoleID"] = new SelectList(_context.Roles, "Id", "Name");
            return RedirectToAction("RoleManagement", "UserRole", new { id = user.Id });
        }

        public async Task<IActionResult> RemoveRoleFromUser([Bind("UserID,RoleID")] string userID, string roleID)
        {
            if (userID == null || roleID == null)
            {
                return RedirectToAction("Error404", "Error");
            }

            var user = await _context.Users.FindAsync(userID);
            var role = await _context.Roles.FindAsync(roleID);
            if (user == null || role == null)
            {
                return RedirectToAction("Error404", "Error");
            }

            await _userManager.RemoveFromRoleAsync(user, role.Name);
            TempData["Success"] = $"{role.Name} has been removed from {user.UserName}";


            ViewData["User"] = user;
            ViewData["Roles"] = await _userManager.GetRolesAsync(user);
            ViewData["RoleID"] = new SelectList(_context.Roles, "Id", "Name");
            return RedirectToAction("RoleManagement", "UserRole", new { id = user.Id });
        }
    }
}