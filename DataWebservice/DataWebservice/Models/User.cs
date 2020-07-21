using DataWebservice.Models.apiDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models
{
    public class User 
    {

        public int userID { get; set; }
        public string displayName { get; set; }

        public string password { get; set; }

        public Boolean isAdmin { get; set; }

        public List<RoomAccess> roomAccess { get; set; }

        public UserDTO ToDTO()
        {
            UserDTO userDTO = new UserDTO();
            userDTO.userID = this.userID;
            userDTO.name = this.displayName;
            userDTO.isAdmin = this.isAdmin;
            //foreach (Room room in this.rooms)
            //{
            //    userDTO.rooms.Add(room.ToDTO());
            //}
            
            return userDTO;
        }
    }
}
