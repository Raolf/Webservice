﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataWebservice.Data;
using DataWebservice.Models;
using Microsoft.EntityFrameworkCore.Internal;
using DataWebservice.Models.apiDTOs;

namespace DataWebservice.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly DataWebserviceContext _context;

        public RoomsController(DataWebserviceContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoom()
        {
            return await _context.Room.ToListAsync();
        }


        // GET: api/Rooms/DTO
        [HttpGet("DTO")]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRoomDTO()
        {
            var rooms = await _context.Room.ToListAsync();
            List<RoomDTO> roomDTOs = new List<RoomDTO>();
            foreach (Room room in rooms)
            {
                roomDTOs.Add(room.ToDTO());
            }
            return roomDTOs;
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            var room = await _context.Room.Where(r => r.roomID == id)
                .Include(r => r.sensors)
                .ThenInclude(s => s.data)
                .Include(r => r.sensors)
                .ThenInclude(sl => sl.sensorLog)
                .Include(r => r.roomAccess)
                .ThenInclude(ra => ra.user)
                .FirstOrDefaultAsync();
    
            if (room == null)
            {
                return NotFound();
            }

            return room.ToDTO();
        }

        // GET: api/Room/5
        [HttpGet("roomsforuser/{id}")]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRoomsForUser(int id)
        {

            var roomAccess = await _context.RoomAccess.Where(ra => ra.userID == id).ToListAsync();
            List<RoomDTO> rooms = new List<RoomDTO>();

            foreach (var item in roomAccess)
            {
                var room = await _context.Room.FindAsync(item.roomID);
                rooms.Add(room.ToDTO());
            }
            return rooms;
        }




        // PUT: api/Rooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.roomID)
            {
                return BadRequest();
            }

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/Rooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            _context.Room.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new { id = room.roomID }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> DeleteRoom(int id)
        {
            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Room.Remove(room);
            await _context.SaveChangesAsync();

            return room;
        }

        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.roomID == id);
        }
    }
}
