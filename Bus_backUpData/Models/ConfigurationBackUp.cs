using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Models
{
    public class ConfigurationBackUp
    {
        public Guid Id { get; set; }
        public string BackupName { get; set; }
        public BackUpSetting BackUpSetting { get; set; }
        public FTPSetting FTPSetting { get; set; }
        public ScheduleBackup ScheduleBackup { get; set; }
        public EmailConfirmation EmailConfirmation { get; set; }
        public bool IsScheduleBackup { get; set; }
        public bool IsEmailConfirmation { get; set; }
        public bool IsEnabled { get; set; } 
    }
}
