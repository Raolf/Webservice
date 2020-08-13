using DataWebservice.Models.apiDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataWebservice.Models
{
    public class Room
    {

        public int roomID { get; set; }
        public string roomName { get; set; }

        public List<RoomAccess> roomAccess { get; set;}

        public List<Sensor> sensors { get; set; }

        public RoomDTO ToDTO()
        {
            RoomDTO roomDTO = new RoomDTO();
            roomDTO.roomID = this.roomID;
            roomDTO.name = this.roomName;
            if(sensors != null)
            foreach(Sensor sensor in this.sensors)
            {
                
                roomDTO.sensors.Add(sensor.ToDTO());
            }
            return roomDTO;
        }
    }
}
