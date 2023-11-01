using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.ViewModels
{
    public enum RunStatus
    {
        [Description("Failed")]
        Failed = 0,
        [Description("Succeeded")]
        Succeeded = 1,
        [Description("Retry (step only)")]
        Retry = 2,
        [Description("Canceled")]
        Canceled = 3,
        [Description("In-progress message")]
        InProgress = 4,
        [Description("Unknown")]
        Unknown = 5,
    }
    public class JobHistoryViewModel
    {
        [DisplayName("Instance id")]
        public int instance_id { get; set; }
        public Guid job_id { get; set; }
        [DisplayName("Job name")]
        public string job_name { get; set; }
        public int step_id { get; set; }
        public string step_name { get; set; }
        public int sql_message_id { get; set; }
        public int sql_severity { get; set; }
        [DisplayName("Message")]
        public string message { get; set; }
        [DisplayName("Run Status")]
        public RunStatus run_status { get; set; }
        [DisplayName("Run Status")]
        public int run_date { get; set; }
        public int run_time { get; set; }
        public int run_duration { get; set; }
        public string operator_emailed { get; set; }
        public string operator_netsent { get; set; }
        public string operator_paged { get; set; }
        public int retries_attempted { get; set; }
        public string server { get; set; }
    }
}
