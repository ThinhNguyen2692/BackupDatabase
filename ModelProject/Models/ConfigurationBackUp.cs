using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Models
{
    public class ConfigurationBackUp : Entity
    {
        public string BackupName { get; set; }
        public Guid BackUpSettingId { get; set; }
        public Guid FTPSettingId { get; set; }
        public Guid ScheduleBackupId { get; set; }
        public Guid EmailConfirmationId { get; set; }
        public Guid DatabaseConnectId { get; set; }
        public bool IsScheduleBackup { get; set; }
        public bool IsEmailConfirmation { get; set; }
        public bool IsEnabled { get; set; }
        [ForeignKey("BackUpSettingId")]
        public virtual BackUpSetting BackUpSetting { get; set; }
        [ForeignKey("FTPSettingId")]
        public virtual FTPSetting FTPSetting { get; set; }
        [ForeignKey("ScheduleBackupId")]
        public virtual ScheduleBackup ScheduleBackup { get; set; }
        [ForeignKey("EmailConfirmationId")]
        public virtual EmailConfirmation EmailConfirmation { get; set; }
        [ForeignKey("DatabaseConnectId")]
        public virtual DatabaseConnect DatabaseConnect { get; set; }
    }
}
