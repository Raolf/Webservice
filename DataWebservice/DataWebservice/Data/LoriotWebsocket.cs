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

namespace DataWebservice.Data
{
    public class LoriotWebsocket
    {
        DataWebserviceContext _context = new DataWebserviceContext();
        Uri uri = new Uri("wss://iotnet.teracom.dk/app?token=vnoS7QAAABFpb3RuZXQudGVyYWNvbS5ka7A2D2ki2C8DUDFO6UOff4g=");
        WebsocketClient clientWS;
        CancellationTokenSource CTSource = new CancellationTokenSource();

        public LoriotWebsocket(DataWebserviceContext _context)
        {
            this._context = _context;
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
            //Task.Run(() => Ping(clientWS));
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
                    data.timestamp = new DateTime(1970, 1, 1, 2, 0, 0, DateTimeKind.Local).AddSeconds((double)loraData.ts / 1000);//Could be improved.
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

        public void SendMessage(Sensor sensor, string setting)
        {
            LoriotDTO msg = new LoriotDTO();
            msg.cmd = "tx";
            msg.EUI = sensor.sensorEUID;
            //msg.confirmed = "false";
            msg.data = setting;
            clientWS.Send(JsonConvert.SerializeObject(msg));

            var log = new SensorLog();

            log.sensorID = sensor.sensorID;
            log.sensor = sensor;
            log.servoSetting = setting;
            log.timestamp = DateTime.Now;

            _context.SensorLog.Add(log);
        }
        public Sensor GetMatchingSensor(Models.Data data, DataWebserviceContext context)
        {
            Sensor sense = context.Sensor.AsQueryable().FirstOrDefault(s => s.sensorEUID == data.sensorEUID);
            if (sense == null)
            {
                sense = new Sensor();
                sense.sensorEUID = data.sensorEUID;
                int count = context.Sensor.AsQueryable().Count();
                if (context.Sensor.AsQueryable().Where(s => s.sensorID == count) == null)
                {
                    sense.sensorID = count;
                }
                else
                {
                    sense.sensorID = context.Sensor.AsQueryable().Count() + 1;
                }
                sense.sensorEUID = data.sensorEUID;
                sense.sensorLog = new List<SensorLog>();

                context.Add(sense);
                context.SaveChanges();
            }
            data.sensor = sense;
            data.sensorID = sense.sensorID;
            return sense;
        }
        public async void Save(Models.Data data)
        {
            
            GetMatchingSensor(data, _context);
            _context.Add(data);
            _context.SaveChanges();
            Console.WriteLine("Added data to DB.\n");
        }
    }
}
