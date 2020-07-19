using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models
{
    public class User 
    {

        public int userID { get; set; }
        public string displayName { get; set; }

        public string password { get; set; }

        public Boolean isAdmin { get; set; }

        public List<RoomAccess> roomAccess { get; set; }
    }
}
