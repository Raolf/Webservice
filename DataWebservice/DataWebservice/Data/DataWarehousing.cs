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
            ExtractRoom();
            ExtractServo();
            ExtractUser();
            ExtractFactTable();

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

        public void IncrementalLoad()
        {
            if (_context.FactTable.Count() > 0)
            {

                //Load data into stage
                ExtractDate(true);
                ExtractRoom();
                ExtractServo();
                ExtractUser();
                ExtractFactTable();

                //Transform
                TransformData();
                TransformRoom();
                TransformServo();
                TransformUser();

                //Load
                //ProcessDateDim();
                ProcessDateDim2();
                ProcessRoomDim();
                ProcessUserDim();
                ProcessServoDim();
                LoadData();
            }

        }


        public void LoadData()
        {
            DateTime NewLoadDate = DateTime.Now;
            DateTime FutureDate = DateTime.MaxValue;

            var factData = _context.Sensor.ToList();
            var factList = new List<DWFactTable>();          
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
                factList.Add(Fact);
                _context.DWFactTable.Add(Fact);
                }
            }
            _context.SaveChanges();
        }


        public void ExtractDate(bool inc)
        {
            var DateList = _context.SensorLog.ToList();
            var dateList = new List<DateDim>();
            if (inc == true)
            {
                
                if(DateList.Count > _context.DateDim.ToList().Count)
                {
                    for(int i = _context.DateDim.ToList().Count; i<DateList.Count; i++)
                    {
                        var date = DateList[i];
                        var Date = new DateDim
                        {
                            //D_ID = 0,
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
                                //D_ID = 0,
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
                            dateList.Add(Date);
                            _context.DateDim.Add(Date);
                        }
            }

            
            _context.SaveChanges();
            //_context.FactTable.BulkInsert(dateList);

        }

        public void ExtractRoom()
        {
            var RoomList = _context.Room.ToList();
            var roomList = new List<RoomDim>();
            foreach (var room in RoomList)
            {

                var Room = new RoomDim
                {
                    //R_ID = 0,
                    RoomID = room.roomID,
                    Name = room.roomName

                };
                roomList.Add(Room);
                _context.RoomDim.Add(Room);
                
            }
            _context.SaveChanges();
            //_context.FactTable.BulkInsert(roomList);

        }

        public void ExtractServo()
        {
            var ServoList = _context.Sensor.ToList();
            var servoList = new List<ServoDim>();
            foreach (var servo in ServoList)
            {
                var sensorlog = _context.SensorLog.FirstOrDefault(r => r.sensorID == servo.sensorID);
                if (sensorlog != null) 
                { 
                    

                var Servo = new ServoDim
                {
                    //S_ID = 0,
                    SensorID = servo.sensorID,
                    PD_ID = 0,
                    DaysSinceSet = 0,
                    HoursSinceSet = 0,
                    SecondsSinceSet = 0,
                    Timestamp = sensorlog.timestamp

                };
                servoList.Add(Servo);
                _context.ServoDim.Add(Servo);
                }
            }
            _context.SaveChanges();
            //_context.FactTable.BulkInsert(servoList);

        }

        public void ExtractUser()
        {
            var userData = _context.User.ToList();
            var userList = new List<UserDim>();
            foreach (var user in userData)
            {

                var User = new UserDim
                {

                    //U_ID = 0,
                    UserID = user.userID,
                    DisplayName = user.displayName,
                    Admin = user.isAdmin

                };
                userList.Add(User);
                _context.UserDim.Add(User);
            }
            _context.SaveChanges();
            //_context.FactTable.BulkInsert(userList);

        }


        public void ExtractFactTable()
        {
            var factData = _context.Sensor.ToList();
            var factList = new List<FactTable>();
            foreach (var fact in factData)
            {
                var data = _context.Data.FirstOrDefault(r => r.sensorID == fact.sensorID);
                if (data != null)
                {
                                 
                var Fact = new FactTable
                {
                    //UniqueID = 0,
                    D_ID = 0,
                    R_ID = 0,
                    S_ID = 0,
                    U_ID = 0,
                    Servosetting = fact.servoSetting,
                    Humidity = data.humidity,
                    CO2 = data.CO2,
                    Temperature = data.temperature
                };
                factList.Add(Fact);
                _context.FactTable.Add(Fact);
                }
            }
            _context.SaveChanges();
            //_context.FactTable.BulkInsert(factList);
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

            var max = _context.Data.Max(sd => sd.timestamp).Date;
            var min = _context.Data.Min(sd => sd.timestamp).Date;
            var temp = min;

            var dateList = new List<DWDateDim>();
            var cultureInfo = new CultureInfo("en-US");
            var calendar = cultureInfo.Calendar;


            while (temp <= max)
            {
                var date = new DWDateDim
                {
                  
                    Day = temp.Day,
                    Month = temp.Month,
                    Monthname = cultureInfo.DateTimeFormat.GetAbbreviatedMonthName(temp.Month),
                    Weekday = Enum.GetName(typeof(DayOfWeek), temp.DayOfWeek),
                    Year = temp.Year,
                    Hour = temp.Hour,
                    Minute = temp.Minute,
                    Seconds = temp.Second,
                    ValidFrom = NewLoadDate,
                    ValidTo = FutureDate
                };
                dateList.Add(date);
                _context.DWDateDim.Add(date);
                temp = temp.AddDays(1);
            }
            _context.SaveChanges();
        }

        public void ProcessDateDim2()
        {
            DateTime NewLoadDate = DateTime.Now;
            DateTime FutureDate = DateTime.MaxValue;


            var dates = _context.DateDim.ToList();

            var dateList = new List<DWDateDim>();
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
                    dateList.Add(date);
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
            var userList = new List<DWUserDim>();

            foreach (var user in users)
            {

                var User = new DWUserDim
                {
                    UserID = user.UserID,
                    DisplayName = user.DisplayName,
                    Admin = user.Admin,
                    ValidFrom = NewLoadDate,
                    ValidTo = FutureDate
                };
                userList.Add(User);
                _context.DWUserDim.Add(User);
            }
            _context.SaveChanges();
        }

        public void ProcessServoDim()
        {
            DateTime NewLoadDate = DateTime.Now;
            DateTime FutureDate = DateTime.MaxValue;

            var list = _context.ServoDim.ToList();
            var servoList = new List<DWServoDim>();
            foreach (var servo in list)
            {

                var Servo = new DWServoDim
                {
                    SensorID = servo.SensorID,
                    PD_ID = 0,
                    DaysSinceSet = 0,
                    HoursSinceSet = 0,
                    SecondsSinceSet = 0,
                    ValidFrom = NewLoadDate,
                    ValidTo = FutureDate

                };
                servoList.Add(Servo);
                _context.DWServoDim.Add(Servo);
            }
            _context.SaveChanges();
        }

        public void ProcessRoomDim()
        {
            DateTime NewLoadDate = DateTime.Now;
            DateTime FutureDate = DateTime.MaxValue;

            var list = _context.RoomDim.ToList();
            var roomList = new List<DWRoomDim>();
            foreach (var room in list)
            {

                var Room = new DWRoomDim
                {
                    RoomID = room.RoomID,
                    Name = room.Name,
                    ValidFrom = NewLoadDate,
                    ValidTo = FutureDate

                };
                roomList.Add(Room);
                _context.DWRoomDim.Add(Room);
            }
            _context.SaveChanges();
        }
    }
}
