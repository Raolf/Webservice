using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models
{
    public class User //: IdentityUser
    {

        public int userID { get; set; }
        public int password { get; set; }
        public bool admin { get; set; }
        public List<RoomAccess> roomAccess { get; set; }
    }
}
