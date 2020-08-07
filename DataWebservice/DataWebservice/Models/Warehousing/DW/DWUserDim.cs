using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models.Warehousing.DW
{
    public class DWUserDim
    {
        public int U_ID { get; set; }
        public int UserID { get; set; }
        public String DisplayName { get; set; }
        public bool Admin { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
