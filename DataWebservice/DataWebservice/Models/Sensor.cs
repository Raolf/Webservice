using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataWebservice.Models.apiDTOs;

namespace DataWebservice.Models
{
    public class Sensor
    {
        public int sensorID { get; set; }
        public string sensorEUID { get; set; }
        public string servoSetting { get; set; }

        public int? roomID { get; set; }

        public Room? room { get; set; }

        public ICollection<Data> data { get; set; }

        public ICollection<SensorLog> sensorLog { get; set; }

        public SensorDTO ToDTO()
        {
            SensorDTO sensorDTO = new SensorDTO();
            sensorDTO.sensorID = this.sensorID;
            sensorDTO.servosetting = this.servoSetting;
            var tempData = data;
            if(tempData != null) { 
                foreach (Data data in tempData)
                {
                 sensorDTO.data.Add(data.DataToDTO());
                }
            }
            return sensorDTO;
        }
    }
}
