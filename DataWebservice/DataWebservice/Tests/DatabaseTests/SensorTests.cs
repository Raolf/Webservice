using DataWebservice.Controllers.API;
using DataWebservice.Data;
using DataWebservice.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Tests.DatabaseTests
{
    public class SensorTests
    {
        private readonly DataWebserviceContext _context;

        public SensorTests(DataWebserviceContext context)
        {
            _context = context;
        }

        [Test]
        public void Can_get_items()
        {

            using (_context)
            {
                SensorsController controller = new SensorsController(_context);

                var sensor = controller.GetSensor();
                var List = new List<Sensor>();

                foreach (var sensor1 in sensor.Result.Value.ToList())
                {
                    List.Add(sensor1);
                }

                Assert.AreEqual(List.Count, List.Count);

            }
        }

        [Test]
        public async void Can_post_items()
        {

            using (_context)
            {
                SensorsController controller = new SensorsController(_context);
                RoomsController roomsController = new RoomsController(_context);

                var room = roomsController.GetRoom();
                var RoomList = new List<Room>();

                foreach (Room data1 in room.Result.Value.ToList())
                {
                    RoomList.Add(data1);
                }

                Models.Sensor testdata = new Models.Sensor();
                testdata.servoSetting = "5";
                testdata.roomID = RoomList[0].roomID;
                await controller.PostSensor(testdata);

                var sensor = controller.GetSensor();
                var List = new List<Sensor>();
                int Listcount = 0;
                foreach (var sensor1 in sensor.Result.Value.ToList())
                {
                    List.Add(sensor1);
                    Listcount++;
                }

                Assert.AreEqual(Listcount, List.Count);
                Assert.AreEqual("5", List[Listcount-1].servoSetting);

                //Delete

                if (List.Count > 0)
                {
                    var item = List[List.Count - 1];

                    await controller.DeleteSensor(item.sensorID);
                }

                sensor = controller.GetSensor();
                List = new List<Sensor>();

                foreach (var sensor1 in sensor.Result.Value.ToList())
                {
                    List.Add(sensor1);
                }


                Assert.AreEqual(Listcount-1, List.Count);
            }
        }
    }
}
