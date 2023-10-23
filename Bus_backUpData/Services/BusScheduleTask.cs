using Bus_backUpData.Func;
using Bus_backUpData.Interface;
using Bus_backUpData.Job;
using Bus_backUpData.Models;
using Bus_backUpData.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ninject;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Quartz.Logging.OperationName;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bus_backUpData.Services
{
    public class BusScheduleTask : IBusScheduleTask
    {
        public async Task<bool> CreateScheduleTaskAsync(ScheduleBackup ScheduleBackup, string JobName)
        {
            // construct a scheduler factory
            try
            {
                string CronString = LibrarySchedule.GetCronString(ScheduleBackup);
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("CreateScheduleTaskAsync__{0}__CronString :{1}", JobName, CronString), Setting.FoderBackUp);        
                var kernel = Nin.InitializeNinjectKernel();
                var scheduler = kernel.Get<IScheduler>();
                await scheduler.ScheduleJob(
                JobBuilder.Create<JobTask>()
                .UsingJobData("jobName", JobName)
                .WithIdentity(JobName)
                .StoreDurably(true)
                .Build(),
                TriggerBuilder.Create()
               .WithIdentity(JobName + "-Trigger")
               .StartAt(ScheduleBackup.FirstDate)
               .WithCronSchedule(CronString)
                .Build()
                );
               
                // start scheduler
                await scheduler.Start();
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("UpdateScheduleTaskAsync__{0}__Error: {1}", JobName, ex.Message), Setting.FoderBackUp);

                return false;
            }

            return true;
        }

        public async Task<bool> UpdateScheduleTaskAsync(ScheduleBackup ScheduleBackup, string JobName)
        {
            try
            {



                string CronString = LibrarySchedule.GetCronString(ScheduleBackup);
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("UpdateScheduleTaskAsync__{0}__CronString :{1}", JobName, CronString), Setting.FoderBackUp);
                var kernel = Nin.InitializeNinjectKernel();
                var scheduler = kernel.Get<IScheduler>();
                ITrigger newTrigger = TriggerBuilder.Create()
                .WithIdentity(JobName + "-Trigger")
                 .StartAt(ScheduleBackup.FirstDate)
                .WithCronSchedule(CronString)
                .Build();

               await scheduler.RescheduleJob(new TriggerKey(JobName + "-trigger"), newTrigger);
                await scheduler.Start();

            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("UpdateScheduleTaskAsync__{0}__Error: {1}", JobName, ex.Message), Setting.FoderBackUp);

                return false;
            }
            return true;
        }

       
        public async Task<bool> CreateScheduleTaskDeleteFTPAsync(int month, int day, string JobName)
        {
            // construct a scheduler factory
            try
            {
                string CronStringDetele = LibrarySchedule.GetCronString(month, day);
                var kernel = Nin.InitializeNinjectKernel();
                var scheduler = kernel.Get<IScheduler>();
                var jobKeyDelete = JobName + "DeleteFTP";

                IJobDetail job = JobBuilder.Create<JobTaskDeleteFTP>()
                .UsingJobData("jobName", JobName)
                .WithIdentity(jobKeyDelete)
                .Build();
                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(jobKeyDelete + "-Trigger")
                .StartAt(DateTime.Now)
                .WithCronSchedule(CronStringDetele)
                .Build();
                 await scheduler.ScheduleJob(job, trigger);
                await scheduler.Start();
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("CreateScheduleTaskDeleteFTPAsync__{0}__Error: {1}", JobName, ex.Message), Setting.FoderBackUp);

                return false;
            }

            return true;
        }

        public async Task<bool> UpdateScheduleTaskDeleteFTPAsync(int month, int day, string JobName)
        {
            try
            {
                var kernel = Nin.InitializeNinjectKernel();
                var scheduler = kernel.Get<IScheduler>();
                var jobKeyDelete = JobName + "DeleteFTP";
                string CronStringDetele = LibrarySchedule.GetCronString(month, day);
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("UpdateScheduleTaskDeleteFTPAsync__{0}__CronStringDetele: {1}", JobName, CronStringDetele), Setting.FoderBackUp);
                ITrigger newTrigger = TriggerBuilder.Create()
                .WithIdentity(jobKeyDelete + "-Trigger")
                .WithCronSchedule(CronStringDetele)
                .StartAt(DateTime.Now)
                .Build();
                await scheduler.RescheduleJob(new TriggerKey(CronStringDetele + "-trigger"), newTrigger);
                await scheduler.Start();

            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("UpdateScheduleTaskDeleteFTPAsync__{0}__Error: {1}", JobName, ex.Message), Setting.FoderBackUp); return false;
            }
            return true;
        }

    }
}
