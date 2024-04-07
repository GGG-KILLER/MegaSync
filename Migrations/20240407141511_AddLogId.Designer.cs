﻿// <auto-generated />
using System;
using MegaSync.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MegaSync.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240407141511_AddLogId")]
    partial class AddLogId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("MegaSync.Model.LogMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Timestamp")
                        .IsDescending();

                    b.ToTable("LogMessages");
                });

            modelBuilder.Entity("MegaSync.Model.MegaLink", b =>
                {
                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Url");

                    b.HasIndex("Path")
                        .IsUnique();

                    b.ToTable("MegaLinks");
                });
#pragma warning restore 612, 618
        }
    }
}
