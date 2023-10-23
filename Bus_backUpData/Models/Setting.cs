using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.Models
{
    public static class Setting
    {
        public static string ConnectionStrings { get;set; }
        public static string DatabaseName { get;set; }
        public static string PathbackUp { get;set; }
        public static string TypeConfigbackup { get;set; }
        public static string TypeConfigFileFTP { get;set; }
        public static string FoderBackUp { get;set; }
        public static string FoderTask { get;set; }
        public static string FoderLogStartUp { get;set; }
    }
    public class SettingEmail
    {
        public static string Email { get; set; }
        public static string PassEmail { get; set; }
        public static string SubjectEmailNoti { get; set; }

        
    }
}
