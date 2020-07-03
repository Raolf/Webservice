using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models
{
    public class RoomAccess
    {
        public int userID { get; set; }
        public User user { get; set; }
        public int roomID { get; set; }
        public Room room { get; set; }
    }
}
