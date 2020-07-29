using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models.apiDTOs
{
    public class DataDTO
    {
        public int HUM_ID { get; set; }
        public int CO2_ID { get; set; }
        public int TEMP_ID { get; set; }
        public DateTime date { get; set; }
        public float HUM_value { get; set; }
        public float CO2_value { get; set; }
        public float TEMP_value { get; set; }
        public DataDTO DataToDTO(Data data)
        {
            DataDTO dataDTO = new DataDTO();
            dataDTO.CO2_ID = data.dataID;
            dataDTO.HUM_ID = data.dataID;
            dataDTO.TEMP_ID = data.dataID;
            dataDTO.date = data.timestamp;
            dataDTO.HUM_value = data.humidity;
            dataDTO.CO2_value = data.CO2;
            dataDTO.TEMP_value = data.temperature;
            return dataDTO;
        }
    }
}
