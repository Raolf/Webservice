using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;
using Newtonsoft.Json;
using DataWebservice.Models;

namespace DataWebservice.Data
{
    public class LoriotWebsocket
    {
        DataWebserviceContext context = new DataWebserviceContext();
        Uri uri = new Uri("wss://iotnet.teracom.dk/app?token=vnoS7QAAABFpb3RuZXQudGVyYWNvbS5ka7A2D2ki2C8DUDFO6UOff4g=");
        WebsocketClient clientWS;
        CancellationTokenSource CTSource = new CancellationTokenSource();

        public LoriotWebsocket()
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
            Task.Run(() => setup());

        }

        async Task setup()
        {
            clientWS.ReconnectTimeout = null;
            clientWS.ErrorReconnectTimeout = TimeSpan.FromSeconds(60);

            setupMessagegRecieve(clientWS);

            clientWS.Start();

        }

        public void setupMessagegRecieve(WebsocketClient client)
        {
            client.MessageReceived.Subscribe(json =>
            {
                LoriotDTO loraData = JsonConvert.DeserializeObject<LoriotDTO>(json.ToString());

                if (loraData.cmd == "rx")
                {
                    Models.Data data = new Models.Data();
                    HexIntoData(loraData.data, data); //LoraData.data is a hex string, data is the webservices data class.
                    data.timestamp = new DateTime(1970, 1, 1, 2, 0, 0, DateTimeKind.Local).AddSeconds((double)loraData.ts / 1000);//Could be improved.
                    //SensorID not being set, String value cannot be converted to int.
                    data.sensorEUID = loraData.EUI;

                    //missing insert of data object into database.
                    context.Add(data);
                }
                else
                {
                    Console.WriteLine("Command was not rx");
                }


            });
        }

        public byte[] HexToByte(string hex)
        {
            int length = hex.Length;
            char[] hexAr= hex.ToCharArray();
            byte[] bytes = new byte[length/2];

            int i = 0;
            int o = 0;
            while (i<length)
            {
                o = i * 2;
                bytes[o] = Convert.ToByte(hexAr[o] << 4+ hexAr[o+1]);
                i =+ 2;
            }
            return bytes;
        }
        public int[] ByteToInt(byte[] bytes)
        {
            int length = bytes.Length;
            byte[] byteToConv = new byte[2]; 
            int[] iAr = new int[length];


            int i = 0;
            int o = 0;
            while (i < length)
            {
                o = i * 2;
                byteToConv[0] = bytes[o];
                byteToConv[1] = bytes[o+1];
                iAr[i] = BitConverter.ToUInt16(byteToConv);
            }
            return iAr;
        }

        public void HexIntoData (String hex, Models.Data data)
        {
            int [] dataArray = ByteToInt(HexToByte(hex));
            if (dataArray.Length < 4)
            {
                Console.WriteLine("Message was too short, did not count 8 bytes");
                return;
            }
            data.humidity = dataArray[0];
            data.temperature = dataArray[1];
            data.CO2 = dataArray[2];
        }



    }
}
