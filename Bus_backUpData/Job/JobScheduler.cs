using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Job
{
    public class JobScheduler
    {
        private readonly IScheduler _scheduler;

        public JobScheduler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public void ScheduleJob<TJob>(string jobName, string groupName, string cronExpression) where TJob : IJob
        {
            var job = JobBuilder.Create<TJob>()
                                .WithIdentity(jobName, groupName)
                                .Build();

            var trigger = TriggerBuilder.Create()
                                        .WithIdentity($"{jobName}_trigger", groupName)
                                        .WithCronSchedule(cronExpression)
                                        .Build();

            _scheduler.ScheduleJob(job, trigger);
        }
    }
}
