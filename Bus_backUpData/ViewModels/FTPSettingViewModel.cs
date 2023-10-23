using Bus_backUpData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.ViewModels
{
    public class FTPSettingViewModel
    {
        [DisplayName("Host Name")]
        public string HostName { get; set; }
        [DisplayName("Protocol")]
        public Protocol Protocol { get; set; }
        [DisplayName("Port")]
        public int Port { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [DisplayName("PassWord")]
        public string PassWord { get; set; }
        [DisplayName("Path")]
        public string Path { get; set; }
        [DisplayName("Auto delete")]
        public bool IsAutoDelete { get; set; }
        [DisplayName("Months")]
        public int Months { get; set; }
        [DisplayName("Days")]
        public int Days { get; set; }
        [DisplayName("Send Backup")]
        public bool SendBackupTypesFull { get; set; }
        [DisplayName("Send Backup")]
        public bool SendBackupTypesFolder { get; set; }
        [DisplayName("Verify backup files on the destination after uploading")]
        public bool VerifyBackup { get; set; }
        [DisplayName("Emergency destination - use only if others have failed")]
        public bool Emergency { get; set; }
        [DisplayName("Data connection")]
        public DataConnection DataConnection { get; set; }
        [DisplayName("speed")]
        public int Speed { get; set; }
        [DisplayName("Keep alive")]
        public bool KeepAlive { get; set; }
        [DisplayName("Interval")]
        public int Interval { get; set; }
    }
}
