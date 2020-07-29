using DataWebservice.Controllers.API;
using DataWebservice.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit;
using Microsoft.AspNetCore.Mvc;
using DataWebservice.Models.apiDTOs;
using NUnit.Framework;
using DataWebservice.Models;

namespace DataWebservice.Tests.DatabaseTests
{
    public class DataTests
    {
        private readonly DataWebserviceContext _context;

        public DataTests(DataWebserviceContext context)
        {
            _context = context;
        }

        [Test]
        public void Can_get_items()
        {

            using (_context)
            {
                DataController controller = new DataController(_context);

                var data = controller.GetData();
                var DTOList = new List<DataDTO>();

                foreach (var data1 in data.Result.Value.ToList())
                {
                    DTOList.Add(data1);
                }

                Assert.AreEqual(DTOList.Count, DTOList.Count);
                //Assert.AreEqual("2", DTOList[0].CO2_value.ToString());

            }
        }

        [Test]
        public async void Can_post_items()
        {

            using (_context)
            {
                DataController controller = new DataController(_context);
                SensorsController sensorsController = new SensorsController(_context);

                // Finding a sensor to atach the data
                var sensors = sensorsController.GetSensor();
                var List = new List<Sensor>();
                var sensorID = 0;

                foreach (var data1 in sensors.Result.Value.ToList())
                {
                    List.Add(data1);
                }
                if (List.Count > 0)
                {
                    var item = List[List.Count - 1];
                    sensorID = item.sensorID;
                   
                }

                Models.Data testdata = new Models.Data();
                testdata.sensorID = sensorID;
                testdata.timestamp = DateTime.Today;
                testdata.humidity = 123;
                testdata.CO2 = 123;
                testdata.temperature = 123;
                
                await controller.PostData(testdata);


                var data = controller.GetData();
                var DTOList = new List<DataDTO>();
                int DTOListcount = 0;
                foreach (var data1 in data.Result.Value.ToList())
                {
                    DTOList.Add(data1);
                    DTOListcount++;
                }

                Assert.AreEqual(DTOListcount, DTOList.Count);
                Assert.AreEqual("123", DTOList[DTOList.Count-1].CO2_value.ToString());

                //Delete
                
                await controller.DeleteData(sensorID,DateTime.Today);

                data = controller.GetData();
                DTOList = new List<DataDTO>();

                foreach (var data1 in data.Result.Value.ToList())
                {
                    DTOList.Add(data1);
                }

                Assert.AreEqual(DTOListcount-1, DTOList.Count);
                

            }
        }

    }
}
