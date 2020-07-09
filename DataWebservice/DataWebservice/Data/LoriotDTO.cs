using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Data
{
    public class LoriotDTO
    {
        public string cmd { get; set; }
        public ulong seqno { get; set; }
        public string EUI { get; set; }
        public ulong ts { get; set; }
        public int fcnt { get; set; }
        public int port { get; set; }
        public ulong freq { get; set; }
        public int rssi { get; set; }
        public float snr { get; set; }
        public double toa { get; set; }
        public string dr { get; set; }
        public bool ack { get; set; }
        public int bat { get; set; }
        public bool offline { get; set; }
        public string data { get; set; }
    }
}
