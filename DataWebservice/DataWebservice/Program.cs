using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using DataWebservice.Data;
using DatawebService.Data;
using System.Configuration;

using DataWebservice.Tests.DatabaseTests;

using DataWebservice.Models;
using NUnit.Framework.Constraints;
using DataWebservice.Controllers;

namespace DataWebservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    

                    var serviceProvider = services.GetRequiredService<IServiceProvider>();
                    var configuration = services.GetRequiredService<IConfiguration>();

                    Seed.InitialSetup(serviceProvider, configuration).Wait();

                    //var context = services.GetRequiredService<DataWebserviceContext>();
                    //DataTests dataTests = new DataTests(context);
                    //dataTests.Can_get_items();
                    //DataTests dataTests2 = new DataTests(host.Services.CreateScope().ServiceProvider.GetRequiredService<DataWebserviceContext>());
                    //dataTests2.Can_post_items();

                    //RoomTests roomTests = new RoomTests(host.Services.CreateScope().ServiceProvider.GetRequiredService<DataWebserviceContext>());
                    //roomTests.Can_get_items();
                    //RoomTests roomTests2 = new RoomTests(host.Services.CreateScope().ServiceProvider.GetRequiredService<DataWebserviceContext>());
                    //roomTests2.Can_post_items();

                    //SensorTests sensorTests = new SensorTests(host.Services.CreateScope().ServiceProvider.GetRequiredService<DataWebserviceContext>());
                    //sensorTests.Can_get_items();
                    //SensorTests sensorTests2 = new SensorTests(host.Services.CreateScope().ServiceProvider.GetRequiredService<DataWebserviceContext>());
                    //sensorTests2.Can_post_items();

                    //UserTests userTests = new UserTests(host.Services.CreateScope().ServiceProvider.GetRequiredService<DataWebserviceContext>());
                    //userTests.Can_get_items();
                    //UserTests userTests2 = new UserTests(host.Services.CreateScope().ServiceProvider.GetRequiredService<DataWebserviceContext>());
                    //userTests2.Can_post_delete_items();

                    Console.WriteLine("Tests complete");


                    //Datawarehousing datawarehousing = new Datawarehousing(host.Services.CreateScope().ServiceProvider.GetRequiredService<DataWebserviceContext>());
                    //datawarehousing.InitialLoad();
                    //datawarehousing.IncrementalLoad();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            var lws = new LoriotWebsocket(host.Services.CreateScope().ServiceProvider.GetRequiredService<DataWebserviceContext>());
            lws.LoriotWebsocketStart();
            Sensor sensor = new Sensor();
            sensor.sensorEUID = "0004A30B00259F36";
            sensor.servoSetting = "00000000";
            sensor.sensorLog = new List<SensorLog>();
            Controllers.API.SensorsController sc = new Controllers.API.SensorsController(host.Services.CreateScope().ServiceProvider.GetRequiredService<DataWebserviceContext>());
            //sc.PostSensor(sensor);
            //lws.SendMessage(sensor);

            host.Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();                  
                });
    }
}
