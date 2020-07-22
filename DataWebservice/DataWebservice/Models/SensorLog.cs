using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataWebservice.Models
{
    public class SensorLog
    {
        
        //[Key, Column(Order = 0)]
        public int sensorID { get; set; }
        public Sensor sensor { get; set; }
        //[Key, Column(Order = 1)]
        public DateTime timestamp { get; set; }
        public string servoSetting { get; set; }

    }
}
