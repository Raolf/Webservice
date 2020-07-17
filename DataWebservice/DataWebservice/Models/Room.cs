using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models
{
    public class Room
    {

        public int roomID { get; set; }
        public string roomName { get; set; }

        public int userID { get; set; }
        public User user { get; set; }
    }
}
