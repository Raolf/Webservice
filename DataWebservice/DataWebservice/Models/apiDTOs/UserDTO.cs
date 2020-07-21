using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models.apiDTOs
{
    public class UserDTO
    {
        public int userID { get; set; }
        public string name { get; set; }
        public Boolean isAdmin { get; set; }
        public List<Room> rooms { get; set; }
    }
}
