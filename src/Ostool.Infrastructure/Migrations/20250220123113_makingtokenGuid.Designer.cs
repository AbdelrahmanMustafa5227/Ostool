﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ostool.Infrastructure.Persistence;

#nullable disable

namespace Ostool.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250220123113_makingtokenGuid")]
    partial class makingtokenGuid
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ostool.Domain.Entities.Advertisement", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PostedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("VendorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("VendorId");

                    b.ToTable("Advertisements");
                });

            modelBuilder.Entity("Ostool.Domain.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AuthProvider")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Ostool.Domain.Entities.Car", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AvgPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Model")
                        .IsUnique();

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Ostool.Domain.Entities.CarSpecs", b =>
                {
                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BodyStyle")
                        .HasColumnType("int");

                    b.Property<int>("Displacement")
                        .HasColumnType("int");

                    b.Property<int>("EngineType")
                        .HasColumnType("int");

                    b.Property<double>("GroundClearance")
                        .HasColumnType("float");

                    b.Property<bool>("HasSunRoof")
                        .HasColumnType("bit");

                    b.Property<int>("Horsepower")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfCylinders")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfGears")
                        .HasColumnType("int");

                    b.Property<int>("SeatingCapacity")
                        .HasColumnType("int");

                    b.Property<double>("TopSpeed")
                        .HasColumnType("float");

                    b.Property<int>("TransmissionType")
                        .HasColumnType("int");

                    b.Property<double>("ZeroToSixty")
                        .HasColumnType("float");

                    b.HasKey("CarId");

                    b.ToTable("CarSpecs");
                });

            modelBuilder.Entity("Ostool.Infrastructure.Authentication.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ExpiresIn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Token")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Ostool.Domain.Entities.Vendor", b =>
                {
                    b.HasBaseType("Ostool.Domain.Entities.AppUser");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VendorName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("Ostool.Domain.Entities.Visitor", b =>
                {
                    b.HasBaseType("Ostool.Domain.Entities.AppUser");

                    b.Property<bool>("SubscribedToNewsletter")
                        .HasColumnType("bit");

                    b.ToTable("Visitors");
                });

            modelBuilder.Entity("Ostool.Domain.Entities.Advertisement", b =>
                {
                    b.HasOne("Ostool.Domain.Entities.Car", "Car")
                        .WithMany("advertisements")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ostool.Domain.Entities.Vendor", "Vendor")
                        .WithMany("Advertisements")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("Ostool.Domain.Entities.CarSpecs", b =>
                {
                    b.HasOne("Ostool.Domain.Entities.Car", "Car")
                        .WithOne("carSpecs")
                        .HasForeignKey("Ostool.Domain.Entities.CarSpecs", "CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("Ostool.Domain.Entities.Car", b =>
                {
                    b.Navigation("advertisements");

                    b.Navigation("carSpecs")
                        .IsRequired();
                });

            modelBuilder.Entity("Ostool.Domain.Entities.Vendor", b =>
                {
                    b.Navigation("Advertisements");
                });
#pragma warning restore 612, 618
        }
    }
}
