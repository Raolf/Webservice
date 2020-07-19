using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataWebservice.Models
{
    public class Data
    {
        public int dataID { get; set; }
        public DateTime timestamp { get; set; }
        public int humidity { get; set; }
        public int CO2 { get; set; }
        public int temperature { get; set; }

        public int sensorID { get; set; }
        public string sensorEUID{ get; set; }
        public Sensor sensor { get; set; }

    }
}
