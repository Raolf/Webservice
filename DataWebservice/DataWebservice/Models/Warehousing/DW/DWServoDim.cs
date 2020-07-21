using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models.Warehousing.DW
{
    public class DWServoDim
    {
        public int S_ID { get; set; }
        public int SensorID { get; set; }
        public int PD_ID { get; set; }
        public int DaysSinceSet { get; set; }
        public int HoursSinceSet { get; set; }
        public int SecondsSinceSet { get; set; }
    }
}
