﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using FA_DB.Data;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230425091400_newmirgration")]
    partial class newmirgration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApi.Models.FavoriteTraningPrograms", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Email");

                    b.ToTable("favoriteTraningPrograms");
                });

            modelBuilder.Entity("WebApi.Models.Server", b =>
                {
                    b.Property<int>("ServerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServerId"));

                    b.HasKey("ServerId");

                    b.ToTable("server");
                });

            modelBuilder.Entity("WebApi.Models.TraningData", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Email");

                    b.ToTable("traningData");
                });

            modelBuilder.Entity("WebApi.Models.TraningProgram", b =>
                {
                    b.Property<int>("TraningProgramId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TraningProgramId"));

                    b.Property<string>("FavoriteTraningProgramsEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ServerId")
                        .HasColumnType("int");

                    b.HasKey("TraningProgramId");

                    b.HasIndex("FavoriteTraningProgramsEmail");

                    b.HasIndex("ServerId");

                    b.ToTable("traningPrograms");
                });

            modelBuilder.Entity("WebApi.Models.TraningTypes.BikeSession", b =>
                {
                    b.Property<int>("BikeSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BikeSessionId"));

                    b.Property<float>("AvgSpeed")
                        .HasColumnType("real");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<float>("Distance")
                        .HasColumnType("real");

                    b.Property<float>("Durration")
                        .HasColumnType("real");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("traningDataEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BikeSessionId");

                    b.HasIndex("traningDataEmail");

                    b.ToTable("bikeSessions");
                });

            modelBuilder.Entity("WebApi.Models.TraningTypes.RunningSession", b =>
                {
                    b.Property<int>("RunningSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RunningSessionId"));

                    b.Property<float>("AvgSpeed")
                        .HasColumnType("real");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<float>("Distance")
                        .HasColumnType("real");

                    b.Property<float>("Durration")
                        .HasColumnType("real");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("traningDataEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RunningSessionId");

                    b.HasIndex("traningDataEmail");

                    b.ToTable("runningSessions");
                });

            modelBuilder.Entity("WebApi.Models.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("ServerId")
                        .HasColumnType("int");

                    b.HasKey("Email");

                    b.HasIndex("ServerId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("WebApi.Models.UserData", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DoB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("Email");

                    b.ToTable("userDatas");
                });

            modelBuilder.Entity("WebApi.Models.FavoriteTraningPrograms", b =>
                {
                    b.HasOne("WebApi.Models.User", "User")
                        .WithOne("FavoriteTraningPrograms")
                        .HasForeignKey("WebApi.Models.FavoriteTraningPrograms", "Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi.Models.TraningData", b =>
                {
                    b.HasOne("WebApi.Models.User", "User")
                        .WithOne("TraningData")
                        .HasForeignKey("WebApi.Models.TraningData", "Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi.Models.TraningProgram", b =>
                {
                    b.HasOne("WebApi.Models.FavoriteTraningPrograms", null)
                        .WithMany("TraningPrograms")
                        .HasForeignKey("FavoriteTraningProgramsEmail");

                    b.HasOne("WebApi.Models.Server", null)
                        .WithMany("TraningPrograms")
                        .HasForeignKey("ServerId");
                });

            modelBuilder.Entity("WebApi.Models.TraningTypes.BikeSession", b =>
                {
                    b.HasOne("WebApi.Models.TraningData", "traningData")
                        .WithMany("BikeSessions")
                        .HasForeignKey("traningDataEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("traningData");
                });

            modelBuilder.Entity("WebApi.Models.TraningTypes.RunningSession", b =>
                {
                    b.HasOne("WebApi.Models.TraningData", "traningData")
                        .WithMany("RunningSessions")
                        .HasForeignKey("traningDataEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("traningData");
                });

            modelBuilder.Entity("WebApi.Models.User", b =>
                {
                    b.HasOne("WebApi.Models.Server", null)
                        .WithMany("Users")
                        .HasForeignKey("ServerId");
                });

            modelBuilder.Entity("WebApi.Models.UserData", b =>
                {
                    b.HasOne("WebApi.Models.User", "User")
                        .WithOne("UserData")
                        .HasForeignKey("WebApi.Models.UserData", "Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi.Models.FavoriteTraningPrograms", b =>
                {
                    b.Navigation("TraningPrograms");
                });

            modelBuilder.Entity("WebApi.Models.Server", b =>
                {
                    b.Navigation("TraningPrograms");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("WebApi.Models.TraningData", b =>
                {
                    b.Navigation("BikeSessions");

                    b.Navigation("RunningSessions");
                });

            modelBuilder.Entity("WebApi.Models.User", b =>
                {
                    b.Navigation("FavoriteTraningPrograms")
                        .IsRequired();

                    b.Navigation("TraningData")
                        .IsRequired();

                    b.Navigation("UserData")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
