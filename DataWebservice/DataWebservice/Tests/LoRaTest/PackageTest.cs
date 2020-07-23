using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataWebservice.Data;
using NUnit.Framework;

namespace DataWebservice.Tests.LoRaTest
{
    
    [TestFixture]
    public class PackageTest
    {
        [SetUp]
        public void SetUp()
        {

        }
        [Test]
        public void ConverterTest()
        {
            string hex = "0029001502e5";
            LoriotWebsocket lws = new LoriotWebsocket(new DataWebserviceContext());
            Models.Data data = lws.HexIntoData(hex);

            Assert.IsTrue(data.humidity == 41);
            Assert.IsTrue(data.CO2 == 21);
            Assert.IsTrue(data.temperature == 741);
        }
    }
}
