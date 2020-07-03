﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models
{
    public class Sensor
    {
        public int sensorID { get; set; }
        public int roomID { get; set; }
        public Room room { get; set; }
        public int servoSetting { get; set; }
        public List<Data> data { get; set; }
        public List<SensorLog> sensorLog { get; set; }
    }
}
