using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataWebservice.Models
{
    public class SensorLog
    {
        
        
        public int sensorID { get; set; }
        public Sensor sensor { get; set; }
        public DateTime timestamp { get; set; }
        public string servoSetting { get; set; }

    }
}
