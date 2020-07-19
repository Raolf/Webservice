using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models
{
    public class Sensor
    {
        public int sensorID { get; set; }
        public string sensorEUID { get; set; }
        public int servoSetting { get; set; }

        public int roomID { get; set; }

        public Room room { get; set; }

        public ICollection<Data> data { get; set; }

        public ICollection<SensorLog> sensorLog { get; set; }

    }
}
