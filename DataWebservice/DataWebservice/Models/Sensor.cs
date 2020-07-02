using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models
{
    public class Sensor
    {
        public int sensorID { get; set; }
        public int roomID { get; set; }
        public int servoSetting { get; set; }
    }
}
