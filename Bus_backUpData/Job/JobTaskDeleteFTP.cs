using ModelProject.Func;
using Bus_backUpData.Interface;
using ModelProject.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Job
{
    public class JobTaskDeleteFTP : IJob
    {
        private readonly IBusConfigViewModel _BusConfig;
        private readonly IBusFTP _BusFTP;
        public JobTaskDeleteFTP(IBusConfigViewModel _BusConfig, IBusFTP _BusFTP)
        {
            this._BusFTP = _BusFTP;
            this._BusConfig = _BusConfig;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var LogName = string.Format("{0}{1}", "LogSchedule", DateTime.Now.ToString("ddMMyyyy"));
          
            JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string jobIdString = dataMap.GetString("jobId");
            var jobId = Guid.Empty;
            Guid.TryParse(jobIdString, out jobId);
            _BusFTP.DeleteFTP(jobId);
            
            return Task.FromResult(true);
        }
    }
}
