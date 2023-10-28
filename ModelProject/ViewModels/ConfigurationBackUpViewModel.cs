using ModelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.ViewModels
{
    public class ConfigurationBackUpViewModel
    {
        public ConfigurationBackUpViewModel()
        {
            BackUpViewModels = new List<BackUpViewModel>();
            BackUpSetting = new BackUpSettingViewModel();
            FTPSetting = new FTPSettingViewModel();
            ScheduleBackup = new ScheduleBackupViewModel();
            EmailConfirmation = new EmailConfirmationViewModel();
            MessageBusViewModel = new MessageBusViewModel();
        }
        public Guid Id { get; set; }
        public string BackupName { get; set; }

        public List<BackUpViewModel> BackUpViewModels { get; set; }
        public BackUpSettingViewModel BackUpSetting { get; set; }
        public FTPSettingViewModel FTPSetting { get; set; }
        public ScheduleBackupViewModel ScheduleBackup { get; set; }
        public EmailConfirmationViewModel EmailConfirmation { get; set; }
        public List<JobHistoryViewModel> JobHistoryViewModels { get; set; }
        public MessageBusViewModel MessageBusViewModel { get; set; }
        public bool IsScheduleBackup { get; set; }
        public bool IsEmailConfirmation { get; set; }
        public bool IsEnabled { get; set; }
    }
}
