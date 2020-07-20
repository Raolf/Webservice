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

namespace DataWebservice.Data
{
    public class Datawarehousing
    {
        private readonly DataWebserviceContext _context;


        public Datawarehousing()
        {
            _context = new DataWebserviceContext();
        }


        public async void InitialLoad()
        {
            //Truncate Tables for a clean start
            await _context.Database.ExecuteSqlRawAsync("truncate table FactTable");
            await _context.Database.ExecuteSqlRawAsync("truncate table FactTable");
            await _context.Database.ExecuteSqlRawAsync("truncate table RoomDim");
            await _context.Database.ExecuteSqlRawAsync("truncate table ServoDim");
            await _context.Database.ExecuteSqlRawAsync("truncate table UserDim");

            ExtractDate();
            ExtractRoom();
            ExtractServo();
            ExtractUser();
            ExtractFactTable();
        }

        public void IncrementalLoad()
        {
            if (_context.FactTable.Count() > 0)
            {

                //Load data into stage
                ExtractDate();
                ExtractRoom();
                ExtractServo();
                ExtractUser();
                ExtractFactTable();
            }

        }





        public void ExtractDate()
        {
            var DateList = _context.SensorLog.ToList();
            var dateList = new List<DateDim>();
            foreach (var date in DateList)
            {
                var Date = new DateDim
                {
                    D_ID = 0,
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
                    R_ID = 0,
                    RoomID = room.roomID,
                    Name = room.roomName

                 };
                roomList.Add(Room);
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

                var Servo = new ServoDim
                {
                    S_ID = 0,
                    SensorID = servo.sensorID,
                    PD_ID = 0,
                    DaysSinceSet = 0,
                    HoursSinceSet = 0,
                    SecondsSinceSet = 0

                };
                servoList.Add(Servo);
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

                    U_ID = 0,
                    UserID = user.userID,
                    DisplayName = user.displayName,
                    Admin = user.isAdmin

                };
                userList.Add(User);
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

                var Fact = new FactTable
                {
                    UniqueID = 0,
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
            }
            _context.SaveChanges();
            //_context.FactTable.BulkInsert(factList);

        }
        public void TransformData()
        {
            var factList = _context.FactTable.ToList();
            foreach (FactTable data in factList) {
                if (data.Humidity == null) {
                    data.Humidity = 0;
                }
                if (data.CO2 == null) { 
                    data.CO2 = 0;
                }
                if (data.Temperature == null) {
                    data.Temperature = 0;
                }
            }
        }
        public void TransformRoom() {
            var roomList = _context.RoomDim.ToList();
            foreach (RoomDim room in roomList) {
                if (room.RoomID == null) {
                    room.RoomID = 0;
                }
                if (room.Name == null) {
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
        public void transformServo()
        {
            var servoList = _context.ServoDim.ToList();
            ServoDim servoTemp = null;
            foreach (ServoDim servo in servoList)
            {
                
                if (servo.SensorID == null)
                {
                    servo.SensorID = 0;
                }
                if (servoTemp != null)
                {
                    //servo.SecondsSinceSet = servo. - _context.DB.GetSensor(Stage_Servo.S_ID).GetSensorLog().GetLastItem();
                } else
                {
                    servo.SecondsSinceSet = 0;
                }
                
                //servo.HoursSinceSet = (int)Math.Round((float) (servo.SecondsSinceSet/3600));
                //servo.DaysSinceSet = (int)Math.Round((float)servo.HoursSinceSet / 24);
                servoTemp = servo;
            }
        }
    }



    //SecondSsinceSet = data.Timestamp.seconds-sensorLog.Timestamp.seconds;
    //HoursSinceSet = data.Timestamp.hours-sensorLog.Timestamp.hours;
    //DaysSince = data.Timestamp.hours-sensorLog.Timestamp.hours
}
