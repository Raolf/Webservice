using System;

namespace DataWebservice.Models
{
    public class SensorLog
    {
        public int SensorID { get; set; }
        public DateTime timestamp { get; set; }
        public bool servoSetting { get; set; }

    }
}
