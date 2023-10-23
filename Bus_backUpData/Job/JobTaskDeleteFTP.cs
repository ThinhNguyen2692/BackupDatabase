using Bus_backUpData.Func;
using Bus_backUpData.Interface;
using Bus_backUpData.Models;
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
            string jobName = dataMap.GetString("jobName");
           
            _BusFTP.DeleteFTP(jobName);
            
            return Task.FromResult(true);
        }
    }
}
