using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ModelProject.Func;
using Bus_backUpData.Interface;
using ModelProject.Models;
using Bus_backUpData.Services;
using Microsoft.Extensions.Options;
using Quartz;

namespace Bus_backUpData.Job
{
    public class JobTask : IJob
    {
        private readonly IBusFTP _BusFTP;

        public JobTask( IBusFTP _BusFTP)
        {
            this._BusFTP = _BusFTP;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var LogName = string.Format("{0}{1}", "LogSchedule", DateTime.Now.ToString("ddMMyyyy"));

            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string jobName = dataMap.GetString("jobName");
            WriteLogFile.WriteLog(LogName, "JobTask_PushFPT_Start------------" + jobName + "------------------------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderTask);
            _BusFTP.JobTaskPushFTp(jobName);
            WriteLogFile.WriteLog(LogName, "JobTask_PushFPT_End----------------" + jobName + "------------------------" + DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), Setting.FoderTask);
            //Write your custom code here
        }
    }

}
