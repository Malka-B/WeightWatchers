using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tracking.Data.Entities;

namespace Tracking.Data
{
    public class TrackingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = myComputer; Database = WeightWatchers_Tracking ;Trusted_Connection=True;");
                base.OnConfiguring(optionsBuilder);
            }
        }
        public TrackingContext(DbContextOptions<TrackingContext> options) : base(options)
        {

        }
        public TrackingContext()
        { }

        public DbSet<TrackingEntity> Trackings { get; set; }
    }
}

