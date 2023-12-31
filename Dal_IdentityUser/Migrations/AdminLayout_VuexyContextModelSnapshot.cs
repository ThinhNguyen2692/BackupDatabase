﻿// <auto-generated />
using System;
using DalBackup.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DalBackup.Migrations
{
    [DbContext(typeof(AdminLayout_VuexyContext))]
    partial class AdminLayout_VuexyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("DalBackup.Data.AdminLayout_VuexyUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "678d547d-e0ad-46db-9264-68aff65b4c4b",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f8c01591-1113-47ef-869e-6cbb534b1261",
                            Email = "thinh.nguyenngoc@vnresource.org",
                            EmailConfirmed = false,
                            LockoutEnabled = true,
                            NormalizedEmail = "THINH.NGUYENNGOC@VNRESOURCE.ORG",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAELCVNBGg1fLkRrjVxjJdur5nXX7m03/AdcCx5DoaHLgaaLp1AGZZFr8XLRzRSWb5wQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3645d2de-314e-4bf2-aae7-7256ea46cd7b",
                            TwoFactorEnabled = false,
                            UserName = "Admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ModelProject.Models.BackUpSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("BackUpType")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ConfigurationBackUpId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BackUpSettings");
                });

            modelBuilder.Entity("ModelProject.Models.ConfigurationBackUp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BackUpSettingId")
                        .HasColumnType("TEXT");

                    b.Property<string>("BackupName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DatabaseConnectId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmailConfirmationId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FTPSettingId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEmailConfirmation")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsScheduleBackup")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ScheduleBackupId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BackUpSettingId");

                    b.HasIndex("DatabaseConnectId");

                    b.HasIndex("EmailConfirmationId");

                    b.HasIndex("FTPSettingId");

                    b.HasIndex("ScheduleBackupId");

                    b.ToTable("ConfigurationBackUps");
                });

            modelBuilder.Entity("ModelProject.Models.DatabaseConnect", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("DatabaseName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ServerConnectId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ServerConnectId");

                    b.ToTable("DatabaseConnect");
                });

            modelBuilder.Entity("ModelProject.Models.EmailConfirmation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ConfigurationBackUpId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailFailure")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailSuccess")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("EmailConfirmations");
                });

            modelBuilder.Entity("ModelProject.Models.Enum.ScheduleBackupWeekly", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ScheduleBackupId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("WeeklyId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleBackupId");

                    b.HasIndex("WeeklyId");

                    b.ToTable("ScheduleBackupWeeklies");
                });

            modelBuilder.Entity("ModelProject.Models.Enum.Weekly", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Weeklies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c8b1413f-2f1d-41cf-96eb-aa7eaf9714e4"),
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7949), new TimeSpan(0, 7, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "Monday",
                            UpdatedDate = new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7975), new TimeSpan(0, 7, 0, 0, 0)),
                            Value = 2
                        },
                        new
                        {
                            Id = new Guid("a1907086-a621-4a7f-9784-fe7a65a9b01a"),
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7979), new TimeSpan(0, 7, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "Tuesday",
                            UpdatedDate = new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7980), new TimeSpan(0, 7, 0, 0, 0)),
                            Value = 4
                        },
                        new
                        {
                            Id = new Guid("a35339f6-ab5e-4197-825a-424a3888d7fb"),
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7983), new TimeSpan(0, 7, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "Wednesday",
                            UpdatedDate = new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7984), new TimeSpan(0, 7, 0, 0, 0)),
                            Value = 8
                        },
                        new
                        {
                            Id = new Guid("3fe18c8d-cfdc-419a-a848-e07d8b383386"),
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7987), new TimeSpan(0, 7, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "Thursday",
                            UpdatedDate = new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7987), new TimeSpan(0, 7, 0, 0, 0)),
                            Value = 16
                        },
                        new
                        {
                            Id = new Guid("8c6d144e-2fad-4cc2-a1ad-fe7661110f31"),
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7990), new TimeSpan(0, 7, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "Friday",
                            UpdatedDate = new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7990), new TimeSpan(0, 7, 0, 0, 0)),
                            Value = 32
                        },
                        new
                        {
                            Id = new Guid("44ae543b-1c51-4e79-81f0-e1d99e274162"),
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7998), new TimeSpan(0, 7, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "Saturday",
                            UpdatedDate = new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(7999), new TimeSpan(0, 7, 0, 0, 0)),
                            Value = 64
                        },
                        new
                        {
                            Id = new Guid("3c2e8836-3745-451b-826d-da952513e0a6"),
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(8001), new TimeSpan(0, 7, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "Sunday",
                            UpdatedDate = new DateTimeOffset(new DateTime(2024, 1, 2, 8, 34, 16, 270, DateTimeKind.Unspecified).AddTicks(8002), new TimeSpan(0, 7, 0, 0, 0)),
                            Value = 1
                        });
                });

            modelBuilder.Entity("ModelProject.Models.FTPSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ConfigurationBackUpId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Days")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HostName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAutoDelete")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Months")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Port")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Protocol")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FTPSettings");
                });

            modelBuilder.Entity("ModelProject.Models.HistoryFTP", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullFilePathName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("JobId")
                        .HasColumnType("TEXT");

                    b.Property<string>("JobName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("HistoryFTPs");
                });

            modelBuilder.Entity("ModelProject.Models.ServerConnect", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ServerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ServerConnect");
                });

            modelBuilder.Entity("ScheduleBackup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("ActionType")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ConfigurationBackUpId")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("DayEvery")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DayMonth")
                        .HasColumnType("INTEGER");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FirstDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("FreqSubdayInterval")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FreqSubdayType")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("MonthlyDay")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("MonthlyThe")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Occurs")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RecursEveryDay")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RecursEveryWeekly")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TheMonth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TheOrder")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TheWeekly")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ScheduleBackups");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DalBackup.Data.AdminLayout_VuexyUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DalBackup.Data.AdminLayout_VuexyUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DalBackup.Data.AdminLayout_VuexyUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DalBackup.Data.AdminLayout_VuexyUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ModelProject.Models.ConfigurationBackUp", b =>
                {
                    b.HasOne("ModelProject.Models.BackUpSetting", "BackUpSetting")
                        .WithMany()
                        .HasForeignKey("BackUpSettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelProject.Models.DatabaseConnect", "DatabaseConnect")
                        .WithMany("ConfigurationBackUps")
                        .HasForeignKey("DatabaseConnectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelProject.Models.EmailConfirmation", "EmailConfirmation")
                        .WithMany()
                        .HasForeignKey("EmailConfirmationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelProject.Models.FTPSetting", "FTPSetting")
                        .WithMany()
                        .HasForeignKey("FTPSettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScheduleBackup", "ScheduleBackup")
                        .WithMany()
                        .HasForeignKey("ScheduleBackupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BackUpSetting");

                    b.Navigation("DatabaseConnect");

                    b.Navigation("EmailConfirmation");

                    b.Navigation("FTPSetting");

                    b.Navigation("ScheduleBackup");
                });

            modelBuilder.Entity("ModelProject.Models.DatabaseConnect", b =>
                {
                    b.HasOne("ModelProject.Models.ServerConnect", "ServerConnects")
                        .WithMany("DatabaseConnects")
                        .HasForeignKey("ServerConnectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServerConnects");
                });

            modelBuilder.Entity("ModelProject.Models.Enum.ScheduleBackupWeekly", b =>
                {
                    b.HasOne("ScheduleBackup", "ScheduleBackup")
                        .WithMany("ScheduleBackupWeeklies")
                        .HasForeignKey("ScheduleBackupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ModelProject.Models.Enum.Weekly", "Weekly")
                        .WithMany()
                        .HasForeignKey("WeeklyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ScheduleBackup");

                    b.Navigation("Weekly");
                });

            modelBuilder.Entity("ModelProject.Models.DatabaseConnect", b =>
                {
                    b.Navigation("ConfigurationBackUps");
                });

            modelBuilder.Entity("ModelProject.Models.ServerConnect", b =>
                {
                    b.Navigation("DatabaseConnects");
                });

            modelBuilder.Entity("ScheduleBackup", b =>
                {
                    b.Navigation("ScheduleBackupWeeklies");
                });
#pragma warning restore 612, 618
        }
    }
}
