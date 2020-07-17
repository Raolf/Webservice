using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Data
{
    
    public class DWTransform
    {
        object stageDW;

        public void Transform()
        {
            //data = stageDW.Data;
            //sensorlog = stageDW.SensorLog;
            //foreach(Stage_DimData in context.DB.Stage_DataDim){
            //  if(Stage_DataDim.DataID == null){
            //      Stage_DataDim.DataID = "NOVALUE";
            //  }
            //  if(Stage_DataDim.Humidity == null){
            //      Stage_DataDim.Humidity = 0;
            //  }
            //  if(Stage_DataDim.CO2 == null){
            //      Stage_DataDim.CO2 = 0;
            //  }
            //  if(Stage_DataDim.Temperature == null){
            //      Stage_DataDim.Temperature = 0;
            //  }
            //}
            //foreach(Stage_RoomDim in context.DB.Stage_RoomDim){
            //  if(Stage_RoomDim.RoomID == null){
            //      Stage_DataDim.RoomID = "NOVALUE;
            //  }
            //  if(Stage_DataDim.Name == null){
            //      Stage_DataDim.Name= "NOVALUE";
            //  }
            //}
            //foreach(Stage_UserDim in context.DB.Stage_UserDim){
            //  if(Stage_RoomDim.UserID == null){
            //      Stage_DataDim.UserID = "NOVALUE;
            //  }
            //  if(Stage_DataDim.Name == null){
            //      Stage_DataDim.Name = "NOVALUE";
            //  }
            //  if(Stage_DataDim.Admin == null){
            //      Stage_DataDim.Admin = 0;
            //  }
            //}
            //foreach(Stage_ServoDim in context.DB.Stage_ServoDim){
            //  if(Stage_RoomDim.SensorID == null){
            //      Stage_DataDim.SensorID = "NOVALUE;
            //  }
            //  Stage_ServoDim.SecondsSinceSet = Datetime.now-context.DB.GetSensor(Stage_Servo.S_ID).GetSensorLog().GetLastItem();
            //  Stage_ServoDim.HoursSinceSet = Math.Round(Stage_ServoDim.SecondsSinceSet/3600);
            //  Stage_ServoDim.DaysSinceSet = Math.Round(Stage_ServoDim.HoursSinceSet/24);
            //}
            //foreach(Stage_DateDim in context.DB.Stage_DateDim){
            //  
            //  Datetime timestampe = Context.DB.GetDate(Stage_DataDim.D_ID).GetTimestamp();
            //  Stage_DataDim.Year = timestamp.Year();
            //  Stage_DataDim.Month = timestamp.Month();
            //  Stage_DataDim.Day = timestamp.DayOfMonth();
            //  Stage_DataDim.Minute = timestamp.MinuteOfDay();
            //  Stage_DataDim.Seconds = timestamp.Second();
            //  Stage_DataDim.WeekDay = timestamp.DayOfWeek();
            //  Stage_DataDim.MonthName = timestamp.Month().Name;
            //  Stage_DataDim.Holiday = timestamp.isHoliday();//Need Library for this.
            //}



            //SecondSsinceSet = data.Timestamp.seconds-sensorLog.Timestamp.seconds;
            //HoursSinceSet = data.Timestamp.hours-sensorLog.Timestamp.hours;
            //DaysSince = data.Timestamp.hours-sensorLog.Timestamp.hours
        }

    }
}
