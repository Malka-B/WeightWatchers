﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tracking.Data;

namespace Tracking.Data.Migrations
{
    [DbContext(typeof(TrackingContext))]
    [Migration("20200713110620_first_migration")]
    partial class first_migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tracking.Data.Entities.TrackingEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("BMI");

                    b.Property<string>("Comments");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("trend");

                    b.HasKey("Id");

                    b.ToTable("Trackings");
                });
#pragma warning restore 612, 618
        }
    }
}