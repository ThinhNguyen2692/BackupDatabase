using Bus_backUpData.Data;
using Bus_backUpData.Func;
using Bus_backUpData.Interface;
using Bus_backUpData.Job;
using Bus_backUpData.Models;
using Bus_backUpData.Services;
using Bus_backUpData.ViewModels;
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

namespace Bus_backUpData
{
    public static class DependecyInjection
    {
        public static async Task<IServiceCollection> serviceDescriptorsAsync(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Context>();
            services.AddSingleton<IBusBackup, BusBackup>();
            services.AddSingleton<IBusFTP, BusFTP>();
            services.AddSingleton<IBusConfigViewModel, BusConfigViewModel>();
            services.AddSingleton<IBusConfigurationBackUp, BusConfigurationBackUp>();
            services.AddSingleton<IBusHistoryFTP, BusHistoryFTP>();
            services.AddSingleton<IBusScheduleTask, BusScheduleTask>();
            services.AddSingleton<IMyScheduler, MyScheduler>();
            services.AddTransient<IJobFactory, MyJobFactory>();
            services.AddScoped<JobTask>();
            services.AddSingleton<JobTaskDeleteFTP>();

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
