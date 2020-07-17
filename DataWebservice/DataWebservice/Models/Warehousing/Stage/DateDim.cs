using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models.Warehousing.Stage
{
    public class DateDim
    {
        public int D_ID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Seconds { get; set; }
        public String Weekday { get; set; }
        public String Monthname { get; set; }
        public bool Holiday{ get; set; }


    }
}
