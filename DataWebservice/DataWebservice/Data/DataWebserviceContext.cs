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
            modelBuilder.Entity<Room>().ToTable("Room");
         
        }



        public DbSet<DataWebservice.Models.Room> Room { get; set; }
    }
}
