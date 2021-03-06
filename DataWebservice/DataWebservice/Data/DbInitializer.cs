﻿using DataWebservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Data
{
    public class DbInitializer
    {
        public static void Initialize(DataWebserviceContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Rooms.
            if (context.Room.Any())
            {
                return;   // DB has been seeded
            }

            var rooms = new Room[]
            {
            new Room{roomID=1,roomName="420"},
            new Room{roomID=2,roomName="69"},
            new Room{roomID=3,roomName="123"},
            new Room{roomID=4,roomName="987"},
            new Room{roomID=5,roomName="500"},
            new Room{roomID=6,roomName="600"},
            new Room{roomID=7,roomName="700"}

            };
            foreach (Room r in rooms)
            {
                context.Room.Add(r);
            }
            //Cannot insert explicit value for identity column in table 'Room' when IDENTITY_INSERT is set to OFF
            context.SaveChanges();
        }
    }
}
