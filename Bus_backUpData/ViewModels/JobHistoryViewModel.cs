using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.ViewModels
{
    public class JobHistoryViewModel
    {
        public int instance_id { get; set; }
        public Guid job_id { get; set; }
        public string job_name { get; set; }
        public int step_id { get; set; }
        public string step_name { get; set; }
        public int sql_message_id { get; set; }
        public int sql_severity { get; set; }
        public string message { get; set; }
        public int run_status { get; set; }
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
