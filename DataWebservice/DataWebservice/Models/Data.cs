using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models
{
    public class Data
    {
        public int sensorID { get; set; }
        public DateTime timestamp { get; set; }
        public int humidity { get; set; }
        public int CO2 { get; set; }
        public int temperature { get; set; }

    }
}
