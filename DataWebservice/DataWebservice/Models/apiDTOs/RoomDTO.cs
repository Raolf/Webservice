using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models.apiDTOs
{
    public class RoomDTO
    {
        public int roomID { get; set; }
        public string name { get; set; }
        public List<SensorDTO> sensors { get; set; }
    }
}
