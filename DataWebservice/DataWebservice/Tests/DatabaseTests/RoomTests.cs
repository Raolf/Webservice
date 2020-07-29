using DataWebservice.Controllers.API;
using DataWebservice.Data;
using DataWebservice.Models;
using DataWebservice.Models.apiDTOs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Tests.DatabaseTests
{
    public class RoomTests
    {
        private readonly DataWebserviceContext _context;

        public RoomTests(DataWebserviceContext context)
        {
            _context = context;
        }

        [Test]
            public void Can_get_items()
            {

                using (_context)
                {
                    var controller = new RoomsController(_context);

                    var room = controller.GetRoom();

                    var List = new List<Room>();

                    foreach (Room data1 in room.Result.Value.ToList())
                    {
                        List.Add(data1);
                    }

                    Assert.AreEqual(List.Count, List.Count);
                }
                
            }

        [Test]
        public async void Can_post_items()
        {

            using (_context)
            {
                var controller = new RoomsController(_context);

                Models.Room testdata = new Models.Room();
                testdata.roomName = "testroom";
                await controller.PostRoom(testdata);


                var room = controller.GetRoom();

                var List = new List<Room>();
                int Listcount = 0;
                foreach (Room data1 in room.Result.Value.ToList())
                {
                    List.Add(data1);
                    Listcount++;
                }

                Assert.AreEqual(Listcount, List.Count);
                Assert.AreEqual("testroom", List[Listcount-1].roomName);

                //Delete

                if (List.Count > 0)
                {
                    var item = List[List.Count - 1];

                    await controller.DeleteRoom(item.roomID);
                }

                room = controller.GetRoom();
                List = new List<Room>();

                foreach (Room data1 in room.Result.Value.ToList())
                {
                    List.Add(data1);
                }


                Assert.AreEqual(Listcount-1, List.Count);
            }
        }


    }
}
