﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models.apiDTOs
{
    public class SensorDTO
    {
        public SensorDTO()
        {
            data = new List<DataDTO>();
        }

        public int sensorID { get; set; }
        public List<DataDTO> data { get; set; }
        public string servosetting { get; set; }

    }
}
