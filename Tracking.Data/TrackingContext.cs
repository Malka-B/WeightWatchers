using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tracking.Data.Entities;

namespace Tracking.Data
{
    public class TrackingContext : DbContext
    {
        
        public TrackingContext(DbContextOptions<TrackingContext> options) : base(options)
        {

        }
        public TrackingContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = C1; Database = Tracking ;Trusted_Connection=True; ");
            }
        }



        public DbSet<TrackingEntity> Trackings { get; set; }
    }
}

