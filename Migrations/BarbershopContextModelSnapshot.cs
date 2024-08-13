﻿// <auto-generated />
using System;
using Barbershop.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Barbershop.API.Migrations
{
    [DbContext(typeof(BarbershopContext))]
    partial class BarbershopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppointmentService", b =>
                {
                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("AppointmentId", "ServiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("AppointmentService");
                });

            modelBuilder.Entity("Barbershop.API.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BarberId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime>("Schedule")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.HasIndex("BarberId");

                    b.HasIndex("ClientId");

                    b.ToTable("Appointment", (string)null);
                });

            modelBuilder.Entity("Barbershop.API.Models.Barber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "IX_Client_Email")
                        .IsUnique();

                    b.HasIndex(new[] { "Phone" }, "IX_Client_Phone")
                        .IsUnique();

                    b.ToTable("Barber", (string)null);
                });

            modelBuilder.Entity("Barbershop.API.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "IX_Client_Email")
                        .IsUnique();

                    b.HasIndex(new[] { "Phone" }, "IX_Client_Phone")
                        .IsUnique();

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("Barbershop.API.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sevice", (string)null);
                });

            modelBuilder.Entity("AppointmentService", b =>
                {
                    b.HasOne("Barbershop.API.Models.Appointment", null)
                        .WithMany()
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired()
                        .HasConstraintName("FK_AppointmentService_AppointmentId");

                    b.HasOne("Barbershop.API.Models.Service", null)
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_AppointmentService_ServiceId");
                });

            modelBuilder.Entity("Barbershop.API.Models.Appointment", b =>
                {
                    b.HasOne("Barbershop.API.Models.Barber", "Barber")
                        .WithMany("Appointments")
                        .HasForeignKey("BarberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Appointments_Barber");

                    b.HasOne("Barbershop.API.Models.Client", "Client")
                        .WithMany("Appointments")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Appointments_Client");

                    b.Navigation("Barber");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Barbershop.API.Models.Barber", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("Barbershop.API.Models.Client", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
