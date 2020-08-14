using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataWebservice.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DataWebservice.Models.Warehousing.Stage;
using DataWebservice.Models.Warehousing.DW;

namespace DataWebservice.Data
{
    public class DataWebserviceContext : IdentityDbContext<IdentityUser>
    {
        public DataWebserviceContext (DbContextOptions<DataWebserviceContext> options)
            : base(options)
        {
        }


        public DataWebserviceContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoomAccess>().HasKey(ra => new { ra.roomID, ra.userID });

            modelBuilder.Entity<RoomAccess>()
                .HasOne(u => u.user)
                .WithMany(ra => ra.roomAccess)
                .HasForeignKey(r => r.userID);

            modelBuilder.Entity<RoomAccess>()
                .HasOne(r => r.room)
                .WithMany(ra => ra.roomAccess)
                .HasForeignKey(r => r.roomID);

            modelBuilder.Entity<Models.Data>().HasKey(sd => new { sd.sensorID, sd.timestamp });

            modelBuilder.Entity<Models.Data>()
                .HasOne(r => r.sensor)
                .WithMany(sd => sd.data)
                .HasForeignKey(r => r.sensorID);

            modelBuilder.Entity<SensorLog>().HasKey(sl => new { sl.sensorID, sl.timestamp });

            modelBuilder.Entity<SensorLog>()
                .HasOne(s => s.sensor)
                .WithMany(sl => sl.sensorLog)
                .HasForeignKey(s => s.sensorID);


            modelBuilder.Entity<Room>().ToTable("Room");

            modelBuilder.Entity<Models.Data>().ToTable("Data");

            modelBuilder.Entity<RoomAccess>().ToTable("RoomAccess");

            modelBuilder.Entity<Sensor>().ToTable("Sensor");

            modelBuilder.Entity<SensorLog>().ToTable("SensorLog");

            modelBuilder.Entity<User>().ToTable("User");

            //Stage
            modelBuilder.Entity<FactTable>().HasKey(ft => ft.UniqueID);

            modelBuilder.Entity<DateDim>().HasKey(de => de.D_ID);

            modelBuilder.Entity<RoomDim>().HasKey(rd => rd.R_ID);

            modelBuilder.Entity<ServoDim>().HasKey(sd => sd.S_ID);

            modelBuilder.Entity<UserDim>().HasKey(ud => ud.U_ID);

            //DW 
            modelBuilder.Entity<DWFactTable>().HasKey(ft => ft.UniqueID);

            modelBuilder.Entity<DWDateDim>().HasKey(dd => dd.D_ID);

            modelBuilder.Entity<DWRoomDim>().HasKey(rd => rd.R_ID);

            modelBuilder.Entity<DWServoDim>().HasKey(sd => sd.S_ID);

            modelBuilder.Entity<DWUserDim>().HasKey(ud => ud.U_ID);

        }

        public DbSet<DataWebservice.Models.Room> Room { get; set; }
        public DbSet<DataWebservice.Models.Data> Data { get; set; }
        public DbSet<DataWebservice.Models.RoomAccess> RoomAccess { get; set; }
        public DbSet<DataWebservice.Models.Sensor> Sensor { get; set; }
        public DbSet<DataWebservice.Models.SensorLog> SensorLog { get; set; }
        public DbSet<DataWebservice.Models.User> User { get; set; }

        public DbSet<FactTable> FactTable { get; set; }
        public DbSet<DateDim> DateDim { get; set; }
        public DbSet<RoomDim> RoomDim { get; set; }
        public DbSet<ServoDim> ServoDim { get; set; }
        public DbSet<UserDim> UserDim { get; set; }

        public DbSet<DWFactTable> DWFactTable { get; set; }
        public DbSet<DWDateDim> DWDateDim { get; set; }
        public DbSet<DWRoomDim> DWRoomDim { get; set; }
        public DbSet<DWServoDim> DWServoDim { get; set; }
        public DbSet<DWUserDim> DWUserDim { get; set; }

    }
}
