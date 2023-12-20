using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Models
{
    public enum Protocol
    {
        [Description("FTP (Standard)")]
        FTPStandard = 0,
        [Description("FTPS (Implicit)")]
        FTPSImplicit = 1 ,
        [Description("FTPS (Explicit)")]
        FTPSExplicit = 2,
        [Description("SFTP")]
        SFTP,
    }
    public enum DataConnection
    {
        Passive,
        Active
    }
    public class FTPSetting : Entity
    {
        public string HostName { get; set; }
        public Protocol Protocol { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Path { get; set; }
        public bool IsAutoDelete { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }
        public Guid ConfigurationBackUpId { get; set; }
    }

}
