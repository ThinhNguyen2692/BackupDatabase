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
        private readonly IBusFTP _busFTP;
        private readonly IMyScheduler _myScheduler;
        public BusScheduleTask(IBusFTP _busFTP, IMyScheduler myScheduler)
        {
            this._busFTP = _busFTP;
            _myScheduler = myScheduler;
        }
        public async Task<bool> CreateScheduleTaskAsync(ScheduleBackup ScheduleBackup, string JobName)
        {
            // construct a scheduler factory
            try
            {
                // StdSchedulerFactory factory = new StdSchedulerFactory();
                // IScheduler scheduler = await factory.GetScheduler();
                //await scheduler.Start();
                string CronString = LibrarySchedule.GetCronString(ScheduleBackup);
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("CreateScheduleTaskAsync__{0}__CronString :{1}", JobName, CronString), Setting.FoderBackUp);
              
               // IJobDetail job = JobBuilder.Create<JobTask>()
               // .UsingJobData("jobName", JobName)
               // .WithIdentity(JobName)
               // .StoreDurably(true)
               // .Build();
               // ITrigger trigger = TriggerBuilder.Create()
               //.WithIdentity("trigger1")
               // .StartNow()
               // .WithSimpleSchedule(x => x
               //     .WithIntervalInSeconds(10)
               //     .RepeatForever())
               // .Build();
                //await scheduler.ScheduleJob(job, trigger);
                //  await Task.Delay(TimeSpan.FromSeconds(60));



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

                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();
                scheduler.Start();
                string CronString = LibrarySchedule.GetCronString(ScheduleBackup);
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("UpdateScheduleTaskAsync__{0}__CronString :{1}", JobName, CronString), Setting.FoderBackUp);
                ITrigger newTrigger = TriggerBuilder.Create()
                .WithIdentity(JobName + "-Trigger")
                .WithCronSchedule(CronString)
                .Build();
                scheduler.RescheduleJob(new TriggerKey(JobName + "-trigger"), newTrigger);

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
                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();
                scheduler.Start();
                var jobKeyDelete = JobName + "DeleteFTP";

                IJobDetail job = JobBuilder.Create<JobTaskDeleteFTP>()
                .UsingJobData("jobName", JobName)
                .WithIdentity(jobKeyDelete)
                .Build();
                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(jobKeyDelete + "-Trigger")
                .WithCronSchedule(CronStringDetele)
                .Build();
                scheduler.ScheduleJob(job, trigger);
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
                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();
                scheduler.Start();
                var jobKeyDelete = JobName + "DeleteFTP";
                string CronStringDetele = LibrarySchedule.GetCronString(month, day);
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("UpdateScheduleTaskDeleteFTPAsync__{0}__CronStringDetele: {1}", JobName, CronStringDetele), Setting.FoderBackUp);
                ITrigger newTrigger = TriggerBuilder.Create()
                .WithIdentity(jobKeyDelete + "-Trigger")
                .WithCronSchedule(CronStringDetele)
                .Build();
                scheduler.RescheduleJob(new TriggerKey(CronStringDetele + "-trigger"), newTrigger);

            }
            catch (Exception ex)
            {
                WriteLogFile.WriteLog(string.Format("{0}{1}", "LogBackUp", DateTime.Now.ToString("ddMMyyyy")), string.Format("UpdateScheduleTaskDeleteFTPAsync__{0}__Error: {1}", JobName, ex.Message), Setting.FoderBackUp); return false;
            }
            return true;
        }

    }
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
}
