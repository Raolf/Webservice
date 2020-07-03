using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models
{
    public class Room
    {
        public int roomID { get; set; }
        public int name { get; set; }
        public List<RoomAccess> roomAccess { get; set; }
        public List<Sensor> sensors { get; set; }
    }
}
