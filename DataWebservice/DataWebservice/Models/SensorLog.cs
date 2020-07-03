using System;

namespace DataWebservice.Models
{
    public class SensorLog
    {
        public int sensorID { get; set; }
        public Sensor sensor { get; set; }
        public DateTime timestamp { get; set; }
        public bool servoSetting { get; set; }

    }
}
