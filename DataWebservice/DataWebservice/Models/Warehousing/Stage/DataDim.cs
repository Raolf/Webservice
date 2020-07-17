using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models.Warehousing.Stage
{
    public class DataDim
    {
        public int M_ID { get; set; }
        public int DataID { get; set; }
        public int Humidity { get; set; }
        public int CO2 { get; set; }
        public int Temperature { get; set; }

    }
}
