using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models.Warehousing.Stage
{
    public class FactTable
    {
        public int UniqueID { get; set; }
        public int D_ID { get; set; }
        public int R_ID { get; set; }
        public int S_ID { get; set; }
        public int U_ID { get; set; }
        public string Servosetting { get; set; }
        public int Humidity { get; set; }
        public int CO2 { get; set; }
        public int Temperature { get; set; }

    }
}
