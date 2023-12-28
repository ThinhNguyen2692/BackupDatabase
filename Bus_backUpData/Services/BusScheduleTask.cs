using ModelProject.Func;
using Bus_backUpData.Interface;
using Bus_backUpData.Job;
using ModelProject.Models;
using ModelProject.ViewModels;
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
using DalBackup.Data;
using Quartz.Impl.Matchers;
using Microsoft.Data.Sqlite;
using Quartz.Impl.AdoJobStore;
using Quartz.Impl.AdoJobStore.Common;
using Quartz.Spi;
using Quartz.Simpl;

namespace Bus_backUpData.Services
{
    public class BusScheduleTask : IBusScheduleTask
    {
        private readonly Nin _nin;
        public BusScheduleTask(Nin nin)
        {
            _nin = nin;
        }

        private async Task<IScheduler> GetIScheduler()
        {
            var builder = SchedulerBuilder.Create();
            builder.UsePersistentStore(options =>
            {
                options.UseClustering();
                options.UseSQLite(sqlServerOptions =>
                {
                    sqlServerOptions.ConnectionString = Setting.ConnectionStrings;
                });
                options.UseSerializer<JsonObjectSerializer>();
            });
            var scheduler = builder.BuildScheduler().Result;
            return scheduler;
        }

        public async Task<bool> CreateScheduleTaskAsync(ScheduleBackup ScheduleBackup, string JobName, Guid jobId)
        {
            // construct a scheduler factory
            try
            {
                JobName = $"{JobName}{jobId.ToString().Split('-')[0]}";
                string CronString = LibrarySchedule.GetCronString(ScheduleBackup);
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("CreateScheduleTaskAsync__{0}__CronString :{1}", JobName, CronString), Setting.FoderBackUp);
                var sche = await GetIScheduler();
                var kernel = _nin.InitializeNinjectKernelAsync();
                var scheduler = kernel.Get<IScheduler>();

                await scheduler.ScheduleJob(
                JobBuilder.Create<JobTask>()
                .UsingJobData("jobId", jobId)
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

        public async Task<bool> UpdateScheduleTaskAsync(ScheduleBackup ScheduleBackup, string JobName, Guid jobId)
        {
            try
            {
                JobName = $"{JobName}{jobId.ToString().Split('-')[0]}";
                string CronString = LibrarySchedule.GetCronString(ScheduleBackup);
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("UpdateScheduleTaskAsync__{0}__CronString :{1}", JobName, CronString), Setting.FoderBackUp);
                var sche = await GetIScheduler();
                var kernel = _nin.InitializeNinjectKernelAsync();
                var scheduler = kernel.Get<IScheduler>();
                var jobKey = new JobKey(JobName);
                await scheduler.UnscheduleJob(new TriggerKey(JobName + "-Trigger"));
                await scheduler.DeleteJob(jobKey);

                var jobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
                var Job = JobBuilder.Create<JobTask>()
              .UsingJobData("jobId", jobId)
                .WithIdentity(JobName)
                .StoreDurably(true)
                .Build();
                ITrigger newTrigger = TriggerBuilder.Create()
                .WithIdentity(JobName + "-Trigger")
                 .StartAt(ScheduleBackup.FirstDate)
                .WithCronSchedule(CronString)
                .Build();
                await scheduler.ScheduleJob(Job, newTrigger);
                await scheduler.Start();
            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("UpdateScheduleTaskAsync__{0}__Error: {1}", JobName, ex.Message), Setting.FoderBackUp);

                return false;
            }
            return true;
        }


        public async Task<bool> CreateScheduleTaskDeleteFTPAsync(int month, int day, string JobName, Guid jobId)
        {
            // construct a scheduler factory
            try
            {
                JobName = $"{JobName}{jobId.ToString().Split('-')[0]}";
                string CronStringDetele = LibrarySchedule.GetCronString(month, day);
                var sche = await GetIScheduler();
                var kernel = _nin.InitializeNinjectKernelAsync();
                var scheduler = kernel.Get<IScheduler>();
                var jobKeyDelete = JobName + "DeleteFTP";

                IJobDetail job = JobBuilder.Create<JobTaskDeleteFTP>()
               .UsingJobData("jobId", jobId)
                .WithIdentity(jobKeyDelete)
                .StoreDurably(true)
                .Build();
                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(jobKeyDelete + "-Trigger")
                .StartAt(DateTime.Now)
                .WithCronSchedule(CronStringDetele)
                .Build();
                await scheduler.Start();
                await scheduler.ScheduleJob(job, trigger);

            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("CreateScheduleTaskDeleteFTPAsync__{0}__Error: {1}", JobName, ex.Message), Setting.FoderBackUp);

                return false;
            }

            return true;
        }

        public async Task<bool> UpdateScheduleTaskDeleteFTPAsync(int month, int day, string JobName, Guid jobId)
        {
            try
            {
                JobName = $"{JobName}{jobId.ToString().Split('-')[0]}";
                var sche = await GetIScheduler();
                var kernel = _nin.InitializeNinjectKernelAsync();
                var scheduler = kernel.Get<IScheduler>();
                await scheduler.Start();
                var jobKeyDelete = JobName + "DeleteFTP";

                var jobKey = new JobKey(jobKeyDelete);
                await scheduler.UnscheduleJob(new TriggerKey(jobKeyDelete + "-Trigger"));
                await scheduler.DeleteJob(jobKey);


                string CronStringDetele = LibrarySchedule.GetCronString(month, day);
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("UpdateScheduleTaskDeleteFTPAsync__{0}__CronStringDetele: {1}", JobName, CronStringDetele), Setting.FoderBackUp);
                var Job = JobBuilder.Create<JobTaskDeleteFTP>()
               .UsingJobData("jobId", jobId)
               .WithIdentity(jobKeyDelete)
               .StoreDurably(true)
               .Build();
                ITrigger newTrigger = TriggerBuilder.Create()
                .WithIdentity(jobKeyDelete + "-Trigger")
                .WithCronSchedule(CronStringDetele)
                .StartAt(DateTime.Now)
                .Build();
                await scheduler.ScheduleJob(Job, newTrigger);


            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("UpdateScheduleTaskDeleteFTPAsync__{0}__Error: {1}", JobName, ex.Message), Setting.FoderBackUp); return false;
            }
            return true;
        }
        public async Task<bool> DeleteScheduleTask(string JobName, Guid jobId)
        {
            JobName = $"{JobName}{jobId.ToString().Split('-')[0]}";
            var jobKey = new JobKey(JobName);
            var jobKeyDelete = new JobKey(JobName + "DeleteFTP");
            var sche = await GetIScheduler();
            var kernel = _nin.InitializeNinjectKernelAsync();
            var scheduler = kernel.Get<IScheduler>();
            await scheduler.Start();
            await scheduler.UnscheduleJob(new TriggerKey(JobName + "-Trigger"));
            await scheduler.DeleteJob(jobKey);

            await scheduler.UnscheduleJob(new TriggerKey(JobName + "DeleteFTP" + "-Trigger"));
            await scheduler.DeleteJob(jobKeyDelete);
            return true;
        }
    }
}
