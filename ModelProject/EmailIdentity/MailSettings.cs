using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.EmailIdentity
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public static class MailSettingCreate
    {
        public static string UserName { get; set; }
        public static string Email { get; set; }
        public static string PassWordDefault { get; set; }
    }
}
