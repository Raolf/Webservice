using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;
using Newtonsoft.Json;
using DataWebservice.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataWebservice.Controllers.API;

namespace DataWebservice.Data
{
    public class LoriotWebsocket
    {
        DataWebserviceContext _context = new DataWebserviceContext();
        Uri uri = new Uri("wss://iotnet.teracom.dk/app?token=vnoS7QAAABFpb3RuZXQudGVyYWNvbS5ka7A2D2ki2C8DUDFO6UOff4g=");
        WebsocketClient clientWS;
        CancellationTokenSource CTSource = new CancellationTokenSource();
        SensorsController sc;
        DataController dc;
        SensorLogsController slc;
        

        public LoriotWebsocket(DataWebserviceContext _context)
        {
            this._context = _context;
            sc = new Controllers.API.SensorsController(_context);
            dc = new DataController(_context);
            slc = new SensorLogsController(_context);
        }

        public void LoriotWebsocketStart()
        {
            Func<ClientWebSocket> factory = new Func<ClientWebSocket>(() =>
            { 
                var clientWebSocket = new ClientWebSocket
                {
                    Options =
                    {
                        KeepAliveInterval = TimeSpan.FromSeconds(60)
                    }
                };
                return clientWebSocket;
            });

            clientWS = new WebsocketClient(uri, factory);
        
            clientWS.ReconnectTimeout = null;
            clientWS.ErrorReconnectTimeout = TimeSpan.FromSeconds(60);

            setupMessageRecieve(clientWS);
            clientWS.Start();
            Console.WriteLine("Loriot Running.\n");
        }

        public void setupMessageRecieve(WebsocketClient client)
        {
            client.MessageReceived.Subscribe(json =>
            {
                Console.WriteLine("Message recieved.\n");
                LoriotDTO loraData = JsonConvert.DeserializeObject<LoriotDTO>(json.ToString());

                if (loraData.cmd == "rx")
                {
                    Models.Data data = HexIntoData(loraData.data); //LoraData.data is a hex string, data is the webservices data class.
                    data.timestamp = new DateTime(1970, 1, 1, 2, 0, 0, DateTimeKind.Local).AddSeconds((double) loraData.ts / 1000);//Could be improved.
                    Console.WriteLine("Date is: "+data.timestamp+"\n");
                    data.sensorEUID = loraData.EUI;
                    Task.Run(()=> Save(data));
                }
                else
                {
                    Console.WriteLine("Command was not rx");
                }

            });
        }

        public Models.Data HexIntoData (String hex)
        {
            Models.Data data = new Models.Data();
            string[] dataArray = new string[3];

            for(int i = 0; i<3; i++){
                dataArray[i] = hex.Substring(i*4,4);
            }

            data.humidity = Convert.ToInt32(dataArray[0],16);
            data.temperature = Convert.ToInt32(dataArray[1],16);
            data.CO2 = Convert.ToInt32(dataArray[2],16);
            return data;
        }

        public void SendMessage(Sensor sensor)
        {
            LoriotDTO msg = new LoriotDTO();
            msg.cmd = "tx";
            msg.EUI = sensor.sensorEUID;
            msg.data = sensor.servoSetting;
            msg.port = 3;
            msg.confirmed = false;
            clientWS.Send(JsonConvert.SerializeObject(msg));
            Console.WriteLine("Message sent");

            
            var log = new SensorLog();

            sensor = _context.Sensor.AsQueryable().First(s => s.sensorEUID == sensor.sensorEUID);

            log.servoSetting = sensor.servoSetting;
            log.timestamp = DateTime.Now;

            log.sensorID = sensor.sensorID;
            
             slc.PostSensorLog(log).Wait();
             _context.SaveChanges();
        }
        public Sensor GetMatchingSensor(Models.Data data, DataWebserviceContext context)
        {
            Sensor sense = context.Sensor.AsQueryable().FirstOrDefault(s => s.sensorEUID == data.sensorEUID);
            if (sense == null)
            {
                sense = new Sensor();
                sense.sensorEUID = data.sensorEUID;
                int count = context.Sensor.AsQueryable().Count();
                sense.sensorLog = new List<SensorLog>();
                sense.servoSetting = "00000000";
                
                sc.PostSensor(sense).Wait();
                sense = context.Sensor.AsQueryable().FirstOrDefault(s => s.sensorEUID == data.sensorEUID);
                
                
            }          

            
            return sense;
        }
        public void Save(Models.Data data)
        {
            Sensor sensor = GetMatchingSensor(data, _context);
            data.sensorID = sensor.sensorID;
            _context.Add(data);
            _context.SaveChanges();
            Console.WriteLine("Added data to DB.\n");
            SendMessage(sensor);
            Console.WriteLine("Message Sent\n");
        }
    }
}
