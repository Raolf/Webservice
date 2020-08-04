using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataWebservice.Data;
using DataWebservice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using DataWebservice.Models.apiDTOs;

namespace DataWebservice.Controllers.API
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataWebserviceContext _context;

        public UsersController(DataWebserviceContext context)
        {
            _context = context;

        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/DTO
        [HttpGet("DTO")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUserDTO()
        {
            var users = await _context.User.ToListAsync();

            List<UserDTO> userDTOs = new List<UserDTO>();
            foreach (User user in users)
            {
                userDTOs.Add(user.ToDTO());
            }
            return userDTOs;
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            var roomAccess = await _context.RoomAccess.Where(r => r.userID == user.userID).Include(r => r.room).ToListAsync();

            

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = Authenticate(model.displayName, model.password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(new
            {
                Id = user.userID,
            });
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.userID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.userID }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.userID == id);
        }


        public User Authenticate(string displayName, string password)
        {
            if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.User.SingleOrDefault(x => x.displayName == displayName);

            // check if displayName exists
            if (user == null)
                return null;

            if (password != user.password)
                return null;

            // authentication successful
            return user;
        }


        public class AuthenticateModel
        {
            [Required]
            public string displayName { get; set; }

            [Required]
            public string password { get; set; }
        }
    }


}
