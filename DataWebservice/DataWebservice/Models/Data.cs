using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using DataWebservice.Models.apiDTOs;

namespace DataWebservice.Models
{
    public class Data
    {
        public Data(int dataID, DateTime timestamp, int humidity, int CO2, int temperature, int sensorID, string sensorEUID)
        {
        }

        public Data()
        { }

        public int dataID { get; set; }
        public DateTime timestamp { get; set; }
        public int humidity { get; set; }
        public int CO2 { get; set; }
        public int temperature { get; set; }

        public int sensorID { get; set; }
        public string sensorEUID{ get; set; }
        public Sensor sensor { get; set; }
        public DataDTO DataToDTO()
        {
            DataDTO dataDTO = new DataDTO();
            dataDTO.CO2_ID = this.dataID;
            dataDTO.HUM_ID = this.dataID;
            dataDTO.TEMP_ID = this.dataID;
            dataDTO.date = this.timestamp;
            dataDTO.HUM_value = this.humidity;
            dataDTO.CO2_value = this.CO2;
            dataDTO.TEMP_value = this.temperature;
            return dataDTO;
        }
    }
}
