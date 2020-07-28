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
using DataWebservice.Models;
using NUnit.Framework.Constraints;

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

                    var context = services.GetRequiredService<DataWebserviceContext>();

                    DbInitializer.Initialize(context);    

                    
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            var lws = new LoriotWebsocket(host.Services.CreateScope().ServiceProvider.GetRequiredService<DataWebserviceContext>());//host.Services.GetRequiredService<LoriotWebsocket>();
            lws.LoriotWebsocketStart();
            Sensor sensor = new Sensor();
            sensor.sensorEUID = "0004A30B00259F36";
            sensor.servoSetting = "00000000";
            sensor.sensorLog = new List<SensorLog>();
            sensor.sensorID = 2;
            
            lws.SendMessage(sensor);

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
