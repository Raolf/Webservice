using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models.Warehousing.DW
{
    public class DWFactTable
    {
        public int UniqueID { get; set; }
        public int DataKey { get; set; }

        [Key, ForeignKey("DWDateDim")]
        public int D_ID { get; set; }

        [Key, ForeignKey("DWRoomDim")]
        public int R_ID { get; set; }

        [Key, ForeignKey("DWServoDim")]
        public int S_ID { get; set; }

        [Key, ForeignKey("DWUserDim")]
        public int U_ID { get; set; }
        public string Servosetting { get; set; }
        public int Humidity { get; set; }
        public int CO2 { get; set; }
        public int Temperature { get; set; }
    }
}
