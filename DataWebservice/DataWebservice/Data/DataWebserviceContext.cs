using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataWebservice.Models;

namespace DataWebservice.Data
{
    public class DataWebserviceContext : DbContext
    {
        public DataWebserviceContext (DbContextOptions<DataWebserviceContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoomAccess>().HasKey(ra => new { ra.roomID, ra.userID });

            modelBuilder.Entity<Models.Data>().HasKey(sd => new { sd.sensorID, sd.timestamp });

            modelBuilder.Entity<SensorLog>().HasKey(tl => tl.sensorID);

            modelBuilder.Entity<Room>().ToTable("Room");

            modelBuilder.Entity<Models.Data>().ToTable("Data");

            modelBuilder.Entity<RoomAccess>().ToTable("RoomAccess");

            modelBuilder.Entity<Sensor>().ToTable("Sensor");

            modelBuilder.Entity<SensorLog>().ToTable("SensorLog");

            modelBuilder.Entity<User>().ToTable("User");



        }



        public DbSet<DataWebservice.Models.Room> Room { get; set; }



        public DbSet<DataWebservice.Models.Data> Data { get; set; }



        public DbSet<DataWebservice.Models.RoomAccess> RoomAccess { get; set; }



        public DbSet<DataWebservice.Models.Sensor> Sensor { get; set; }



        public DbSet<DataWebservice.Models.SensorLog> SensorLog { get; set; }



        public DbSet<DataWebservice.Models.User> User { get; set; }
    }
}
