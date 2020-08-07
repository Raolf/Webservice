using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models.Warehousing.DW
{
    public class DWRoomDim
    {
        public int R_ID { get; set; }
        public int RoomID { get; set; }
        public String Name { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
