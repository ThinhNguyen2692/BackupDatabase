using Bus_backUpData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Interface
{
    public interface IBusScheduleTask
    {
        public Task<bool> CreateScheduleTaskAsync(ScheduleBackup ScheduleBackup, string JobName);
        public Task<bool> UpdateScheduleTaskAsync(ScheduleBackup ScheduleBackup, string JobName);
        public Task<bool> CreateScheduleTaskDeleteFTPAsync(int month, int day, string JobName);
        public Task<bool> UpdateScheduleTaskDeleteFTPAsync(int month, int day, string JobName);
        public Task<bool> DeleteScheduleTask(string JobName);
    }
}
