using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataWebservice.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using DataWebservice.Models.Warehousing.Stage;
using DataWebservice.Models;
using System.Runtime;
using DataWebservice.Models.Warehousing.DW;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DataWebservice.Data
{
    public class Datawarehousing
    {
        private readonly DataWebserviceContext _context;


        public Datawarehousing(DataWebserviceContext dataWebserviceContext)
        {
            this._context = dataWebserviceContext;
        }

        public void InitialLoad()
        {
            if (_context.FactTable.Count() == 0)
            {
                //Truncate Tables for a clean start
                _context.Database.ExecuteSqlRawAsync("truncate table FactTable");
                _context.Database.ExecuteSqlRawAsync("truncate table FactTable");
                _context.Database.ExecuteSqlRawAsync("truncate table RoomDim");
                _context.Database.ExecuteSqlRawAsync("truncate table ServoDim");
                _context.Database.ExecuteSqlRawAsync("truncate table UserDim");
                _context.Database.ExecuteSqlRawAsync("truncate table DWFactTable");
                _context.Database.ExecuteSqlRawAsync("truncate table DWFactTable");
                _context.Database.ExecuteSqlRawAsync("truncate table DWRoomDim");
                _context.Database.ExecuteSqlRawAsync("truncate table DWServoDim");
                _context.Database.ExecuteSqlRawAsync("truncate table DWUserDim");
                //Load data into stage
                ExtractDate(false);
                ExtractRoom(false);
                ExtractServo(false);
                ExtractUser(false);
                ExtractFactTable(false);
                //Transform
                TransformData();
                TransformRoom();
                TransformServo();
                TransformUser();
                //Load
                ProcessDateDim();
                ProcessRoomDim();
                ProcessUserDim();
                ProcessServoDim();
                LoadData();
            }
        }

        public void IncrementalLoad()
        {

            //Load data into stage
            ExtractDate(true);
            ExtractRoom(true);
            ExtractServo(true);
            ExtractUser(true);
            ExtractFactTable(true);

            //Transform
            TransformData();
            TransformRoom();
            TransformServo();
            TransformUser();

            //Load           
            ProcessDateDim();
            ProcessRoomDim();
            ProcessUserDim();
            ProcessServoDim();
            LoadData();


        }


        public void LoadData()
        {
            DateTime NewLoadDate = DateTime.Now;
            DateTime FutureDate = DateTime.MaxValue;

            var factData = _context.Sensor.ToList();      
            foreach (var fact in factData)
            {
                var data = _context.Data.FirstOrDefault(r => r.sensorID == fact.sensorID);
                if (data != null)
                {                               

                var Fact = new DWFactTable
                {                   
                    Servosetting = fact.servoSetting,
                    Humidity = data.humidity,
                    CO2 = data.CO2,
                    Temperature = data.temperature,
                    ValidFrom = NewLoadDate,
                    ValidTo = FutureDate
                };
                _context.DWFactTable.Add(Fact);
                }
            }
            _context.SaveChanges();
        }


        public void ExtractDate(bool inc)
        {
            var DateList = _context.SensorLog.ToList();
            if (inc == true)
            {
                if (DateList.Count > _context.DateDim.ToList().Count)
                {
                    for (int i = _context.DateDim.ToList().Count; i < DateList.Count; i++)
                    {
                        var date = DateList[i];
                        var Date = new DateDim
                        {
                            Year = date.timestamp.Year,
                            Month = date.timestamp.Month,
                            Day = date.timestamp.Day,
                            Hour = date.timestamp.Hour,
                            Minute = date.timestamp.Minute,
                            Seconds = date.timestamp.Second,
                            Weekday = date.timestamp.ToString("dddd"),
                            Monthname = date.timestamp.ToString("MMMM"),
                            Holiday = false

                        };
                        _context.DateDim.Add(Date);
                    }
                }
            }
            else
            {
                foreach (var date in DateList)
                {
                    var Date = new DateDim
                    {
                        Year = date.timestamp.Year,
                        Month = date.timestamp.Month,
                        Day = date.timestamp.Day,
                        Hour = date.timestamp.Hour,
                        Minute = date.timestamp.Minute,
                        Seconds = date.timestamp.Second,
                        Weekday = date.timestamp.ToString("dddd"),
                        Monthname = date.timestamp.ToString("MMMM"),
                        Holiday = false

                    };
                    _context.DateDim.Add(Date);
                }
            }
            _context.SaveChanges();
        }

        public void ExtractRoom(bool inc)
        {
            var RoomList = _context.Room.ToList();
            if (inc == true)
            {

                if (RoomList.Count > _context.RoomDim.ToList().Count)
                {
                    for (int i = _context.RoomDim.ToList().Count; i < RoomList.Count; i++)
                    {
                        var room = RoomList[i];
                        var Room = new RoomDim
                        {
                            RoomID = room.roomID,
                            Name = room.roomName

                        };
                        _context.RoomDim.Add(Room);
                    }
                }
            }
            else
            {
                foreach (var room in RoomList)
                {

                    var Room = new RoomDim
                    {
                        RoomID = room.roomID,
                        Name = room.roomName

                    };
                    _context.RoomDim.Add(Room);

                }
            }
            _context.SaveChanges();
        }

        public void ExtractServo(bool inc)
        {
            var ServoList = _context.Sensor.ToList();
            if (inc == true)
            {
                if (ServoList.Count > _context.ServoDim.ToList().Count)
                {
                    for (int i = _context.ServoDim.ToList().Count; i < ServoList.Count; i++)
                    {
                        var servo = ServoList[i];
                        var sensorlog = _context.SensorLog.FirstOrDefault(r => r.sensorID == servo.sensorID);
                        if (sensorlog != null)
                        {
                            var Servo = new ServoDim
                            {
                                SensorID = servo.sensorID,
                                PD_ID = 0,
                                DaysSinceSet = 0,
                                HoursSinceSet = 0,
                                SecondsSinceSet = 0,
                                Timestamp = sensorlog.timestamp

                            };
                            _context.ServoDim.Add(Servo);
                        }
                    }
                }
            }
            else
            {
                foreach (var servo in ServoList)
                {
                    var sensorlog = _context.SensorLog.FirstOrDefault(r => r.sensorID == servo.sensorID);
                    if (sensorlog != null)
                    {
                        var Servo = new ServoDim
                        {
                            SensorID = servo.sensorID,
                            PD_ID = 0,
                            DaysSinceSet = 0,
                            HoursSinceSet = 0,
                            SecondsSinceSet = 0,
                            Timestamp = sensorlog.timestamp

                        };
                        _context.ServoDim.Add(Servo);
                    }
                }
            }
            
            _context.SaveChanges();
        }

        public void ExtractUser(bool inc)
        {
            var userData = _context.User.ToList();
            if (inc == true)
            {
                if (userData.Count > _context.UserDim.ToList().Count)
                {
                    for (int i = _context.UserDim.ToList().Count; i < userData.Count; i++)
                    {
                        var user = userData[i];
                        var User = new UserDim
                        {
                            UserID = user.userID,
                            DisplayName = user.displayName,
                            Admin = user.isAdmin

                        };
                        _context.UserDim.Add(User);
                    }
                }
            }
            else
            {
                foreach (var user in userData)
                {
                    var User = new UserDim
                    {
                        UserID = user.userID,
                        DisplayName = user.displayName,
                        Admin = user.isAdmin

                    };
                    _context.UserDim.Add(User);
                }
            }       
            _context.SaveChanges();
        }


        public void ExtractFactTable(bool inc)
        {
            var Data = _context.Data.ToList();
            if (inc == true)
            {
                if (Data.Count > _context.FactTable.ToList().Count)
                {
                    for (int i = _context.FactTable.ToList().Count; i < Data.Count; i++)
                    {
                        var fact = Data[i];
                        var sensor = _context.Sensor.FirstOrDefault(s => s.sensorID == fact.sensorID);
                        if (sensor != null)
                        {
                            var Fact = new FactTable
                            {
                                D_ID = 0,
                                R_ID = 0,
                                S_ID = 0,
                                U_ID = 0,
                                Servosetting = sensor.servoSetting,
                                Humidity = fact.humidity,
                                CO2 = fact.CO2,
                                Temperature = fact.temperature
                            };
                            _context.FactTable.Add(Fact);
                        }
                    }
                }
            }
            else
            {
                foreach (var fact in Data)
                {
                    var sensor = _context.Sensor.FirstOrDefault(s => s.sensorID == fact.sensorID);
                    if (sensor != null)
                    {
                        var Fact = new FactTable
                        {
                            D_ID = 0,
                            R_ID = 0,
                            S_ID = 0,
                            U_ID = 0,
                            Servosetting = sensor.servoSetting,
                            Humidity = fact.humidity,
                            CO2 = fact.CO2,
                            Temperature = fact.temperature
                        };
                        _context.FactTable.Add(Fact);
                    }
                }
            }           
            _context.SaveChanges();
        }

        public void TransformData()
        {
            var factList = _context.FactTable.ToList();
            foreach (FactTable data in factList)
            {
                if (data.Humidity == null)
                {
                    data.Humidity = 0;
                }
                if (data.CO2 == null)
                {
                    data.CO2 = 0;
                }
                if (data.Temperature == null)
                {
                    data.Temperature = 0;
                }
            }
        }
        public void TransformRoom()
        {
            var roomList = _context.RoomDim.ToList();
            foreach (RoomDim room in roomList)
            {
                if (room.RoomID == null)
                {
                    room.RoomID = 0;
                }
                if (room.Name == null)
                {
                    room.Name = "NOVALUE";
                }
            }
        }

        public void TransformUser()
        {
            var userList = _context.UserDim.ToList();
            foreach (UserDim user in userList)
            {
                if (user.UserID == null)
                {
                    user.UserID = 0;
                }
                if (user.DisplayName == null)
                {
                    user.DisplayName = "NOVALUE";
                }
                if (user.Admin == null)
                {
                    user.Admin = false;
                }
            }
        }
        public void TransformServo()
        {
            var servoList = _context.ServoDim.ToList();
            var sensorList = _context.Sensor.ToList();
            ServoDim servoTemp = null;
            foreach (ServoDim servo in servoList)
            {

                
                if(servoTemp == null)
                {
                    if (servo.SensorID == null)
                    {
                        servo.SensorID = 0;
                    }
                    servo.SecondsSinceSet = 0;
                    servo.HoursSinceSet = 0;
                    servo.DaysSinceSet = 0;

                }
                else
                {
                    if (servoTemp != null 
                        && sensorList.Where(s => s.sensorID == servo.SensorID).FirstOrDefault().sensorID == sensorList.Where(s => s.sensorID == servoTemp.SensorID).FirstOrDefault().sensorID)
                    {
                        int ts;
                        if (sensorList.Where(s => s.sensorID == servo.SensorID).FirstOrDefault().sensorLog.Where(s=>s.timestamp == servo.Timestamp).FirstOrDefault().servoSetting != sensorList.Where(s => s.sensorID == servoTemp.SensorID).ToList().FirstOrDefault().sensorLog.Where(s => s.timestamp == servoTemp.Timestamp).FirstOrDefault().servoSetting)
                        {
                            ts = Convert.ToInt32(servo.Timestamp.Subtract(servoTemp.Timestamp.Date).TotalSeconds);
                            servo.SecondsSinceSet = ts;
                            servo.HoursSinceSet = (int)Math.Round((float)(servo.SecondsSinceSet / 3600));
                            servo.DaysSinceSet = (int)Math.Round((float)servo.HoursSinceSet / 24);
                        }
                        else
                        {
                            ts = Convert.ToInt32(servo.Timestamp.Subtract(servoTemp.Timestamp).Ticks);
                            servo.SecondsSinceSet = ts;
                            servo.HoursSinceSet = (int)Math.Round((float)(servo.SecondsSinceSet / 3600));
                            servo.DaysSinceSet = (int)Math.Round((float)servo.HoursSinceSet / 24);
                        }
                    }
                    else
                    {
                        servo.SecondsSinceSet = 0;
                        servo.HoursSinceSet = 0;
                        servo.DaysSinceSet = 0;
                    }
                }
                servoTemp = servo;
            }
        }

        public void ProcessDateDim()
        {
            DateTime NewLoadDate = DateTime.Now;
            DateTime FutureDate = DateTime.MaxValue;

            var dates = _context.DateDim.ToList();

            var cultureInfo = new CultureInfo("en-US");
            var calendar = cultureInfo.Calendar;
            foreach (var temp in dates)
            {
                var date = new DWDateDim
                {
                    D_ID = temp.D_ID,
                    Day = temp.Day,
                    Month = temp.Month,
                    Monthname = cultureInfo.DateTimeFormat.GetAbbreviatedMonthName(temp.Month),
                    Weekday = temp.Weekday,
                    Year = temp.Year,
                    Hour = temp.Hour,
                    Minute = temp.Minute,
                    Seconds = temp.Seconds,
                    ValidFrom = NewLoadDate,
                    ValidTo = FutureDate
                };                              
                var tempdate = _context.DWDateDim.Find(date.D_ID);
                if (tempdate ==null)
                {
                    _context.DWDateDim.Add(date);
                }                               
            }
            _context.SaveChanges();
        }

        public void ProcessUserDim()
        {
            DateTime NewLoadDate = DateTime.Now;
            DateTime FutureDate = DateTime.MaxValue;

            var users = _context.UserDim.ToList();
            foreach (var user in users)
            {

                var User = new DWUserDim
                {
                    U_ID = user.U_ID,
                    UserID = user.UserID,
                    DisplayName = user.DisplayName,
                    Admin = user.Admin,
                    ValidFrom = NewLoadDate,
                    ValidTo = FutureDate
                };

                var tempdata = _context.DWUserDim.Find(User.U_ID);
                if (tempdata == null)
                {
                    _context.DWUserDim.Add(User);
                }
               
            }
            _context.SaveChanges();
        }

        public void ProcessServoDim()
        {
            DateTime NewLoadDate = DateTime.Now;
            DateTime FutureDate = DateTime.MaxValue;

            var list = _context.ServoDim.ToList();
            foreach (var servo in list)
            {
                var Servo = new DWServoDim
                {
                    S_ID = servo.S_ID,
                    SensorID = servo.SensorID,
                    PD_ID = 0,
                    DaysSinceSet = 0,
                    HoursSinceSet = 0,
                    SecondsSinceSet = 0,
                    ValidFrom = NewLoadDate,
                    ValidTo = FutureDate
                };
                var tempdata = _context.DWServoDim.Find(Servo.S_ID);
                if (tempdata == null)
                {
                    _context.DWServoDim.Add(Servo);
                }             
            }
            _context.SaveChanges();
        }

        public void ProcessRoomDim()
        {
            DateTime NewLoadDate = DateTime.Now;
            DateTime FutureDate = DateTime.MaxValue;

            var list = _context.RoomDim.ToList();
            foreach (var room in list)
            {
                var Room = new DWRoomDim
                {
                    R_ID = room.R_ID,
                    RoomID = room.RoomID,
                    Name = room.Name,
                    ValidFrom = NewLoadDate,
                    ValidTo = FutureDate
                };
                var tempdata = _context.DWRoomDim.Find(Room.R_ID);
                if (tempdata == null)
                {
                    _context.DWRoomDim.Add(Room);
                }             
            }
            _context.SaveChanges();
        }
    }
}
