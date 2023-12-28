using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Job
{
    public class CustomJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;
            var jobType = jobDetail.JobType;

            return (IJob)_serviceProvider.GetRequiredService(jobType);
        }

        public void ReturnJob(IJob job)
        {
            // Optionally, you can implement job cleanup logic here
        }
    }
}
