using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models
{
    public class SensorDTO
    {
        public int sensorID { get; set; }
        public List<DataDTO> data { get; set; }
        public string servosetting { get; set; }

    }
}
