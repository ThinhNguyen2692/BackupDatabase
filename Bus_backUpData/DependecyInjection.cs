﻿using Bus_backUpData.Data;
using ModelProject.Func;
using Bus_backUpData.Interface;
using Bus_backUpData.Job;
using ModelProject.Models;
using Bus_backUpData.Services;
using ModelProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalStoredProcedure.Interface;
using DalStoredProcedure.Services;
using DalBackup.Interface;
using DalBackup.Services;
using DalBackup.Repository;
using NuGet.Protocol.Core.Types;
using DalBackup.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using ModelProject.EmailIdentity;

namespace Bus_backUpData
{
    public static class DependecyInjection
    {
        public static async Task<IServiceCollection> serviceDescriptorsAsync(this IServiceCollection services, IConfiguration configuration, string connectionString)
        {
            services.AddTransient<Context>();
            services.AddDbContext<AdminLayout_VuexyContext>(options => options.UseSqlite(connectionString));
            services.AddDefaultIdentity<AdminLayout_VuexyUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AdminLayout_VuexyContext>();
            services.Configure<IdentityOptions>(options => {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;
                options.SignIn.RequireConfirmedEmail = false; // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });
            services.ConfigureApplicationCookie(options => {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.SlidingExpiration = true;
            });
            services.AddOptions();                                        // Kích hoạt Options

            // đọc config



            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IDalStoredProcedureServices, DalStoredProcedureServices>();
            services.AddScoped<IDalServerConnect, DalServerConnect>();
            services.AddScoped<IDalConfigurationBackUp, DalConfigurationBackUp>();
            services.AddScoped<IDalDatabaseConnect, DalDatabaseConnect>();
            services.AddScoped<IBusConfigViewModel, BusConfigViewModel>();
            services.AddScoped<IBusFTP, BusFTP>();
            services.AddSingleton<IBusConfigurationBackUp, BusConfigurationBackUp>();
           
            services.AddScoped<IBusBackup, BusBackup>();
            services.AddSingleton<IBusHistoryFTP, BusHistoryFTP>();
            services.AddSingleton<IBusScheduleTask, BusScheduleTask>();
            services.AddScoped<JobTask>();
            services.AddScoped<JobTaskDeleteFTP>();
       
           
            services.AddSingleton<IBusStoredProcedureServices, BusStoredProcedureServices>();
            services.AddScoped<IBusConfig, BusConfig>();
           
            Setting.TypeConfigbackup = "config.json";
            Setting.TypeConfigFileFTP = "HistoryFTP.json";
            Setting.FoderBackUp = "BackUp";
            Setting.FoderTask = "Task";
            Setting.FoderLogStartUp = "LogStartUp";
            Setting.PathbackUp = Path.GetFullPath("BackUpSql");
            IBusConfigurationBackUp busConfigurationBackUp = new BusConfigurationBackUp();
            List<ConfigurationBackUp> configurationBackUps = busConfigurationBackUp.LoadJsonBackUp();
            var LogName = string.Format("{0}{1}", "LogAddQuartzStartUp", DateTime.Now.ToString("ddMMyyyy"));
            WriteLogFile.WriteLog(LogName, "Create_build_Start---------------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderLogStartUp);
            try
            {
                if (configurationBackUps != null)
                {
                    string log = "JobName:_{0}. JobNameKey: {1}.CronSchedule: {2}. WithIdentity: {3}";
                    services.AddQuartz(q =>
                    {
                        configurationBackUps.ForEach(b =>
                        {
                            var jobKey = new JobKey(b.BackupName);
                            if (b.IsScheduleBackup == true)
                            {
                                string CronString = LibrarySchedule.GetCronString(b.ScheduleBackup);
                                WriteLogFile.WriteLog(LogName, string.Format(log, b.BackupName, jobKey, CronString, jobKey + "-trigger"), Setting.FoderLogStartUp);
                                q.AddJob<JobTask>(opts => { opts.WithIdentity(jobKey); opts.UsingJobData("jobName", b.BackupName); opts.StoreDurably(true); });
                                q.AddTrigger(opts => opts
                                    .ForJob(jobKey)
                                    .WithIdentity(jobKey + "-trigger")
                                    .WithCronSchedule(CronString));
                            }
                            if (b.FTPSetting.IsAutoDelete == true)
                            {
                                string CronStringDetele = LibrarySchedule.GetCronString(b.FTPSetting.Months, b.FTPSetting.Days);
                                var jobKeyDelete = jobKey + "DeleteFTP";
                                WriteLogFile.WriteLog(LogName, string.Format(log, b.BackupName, jobKeyDelete, CronStringDetele, jobKeyDelete + "-trigger"), Setting.FoderLogStartUp);
                                q.AddJob<JobTaskDeleteFTP>(opts => { opts.WithIdentity(jobKeyDelete); opts.UsingJobData("jobName", b.BackupName); opts.StoreDurably(true); });
                                q.AddTrigger(opts => opts
                                    .ForJob(jobKey)
                                    .WithIdentity(jobKeyDelete + "-trigger")
                                    .WithCronSchedule(CronStringDetele));
                            }
                        });
                    });
                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                }
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog(LogName, string.Format("Create_build_Error_______________:{0}",ex.Message), Setting.FoderLogStartUp);
                throw;
            }
            WriteLogFile.WriteLog(LogName, "Create_build_End--------------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderLogStartUp);
            return services;
        }
    }
}
