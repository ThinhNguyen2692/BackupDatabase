using ModelProject.Models;
using ModelProject.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.ViewModels
{
    public class ScheduleBackupViewModel
    {

        [DisplayName("Occurs")]
        public Occurs Occurs { get; set; }
        [DisplayName("Recurs every")]
        public int RecursEveryDay { get; set; }
        [DisplayName("Recurs every")]
        public int RecursEveryWeekly { get; set; }
        public List<int> Weeklies { get; set; }
        public bool MonthlyDay { get; set; }
        public int DayEvery { get; set; }
        public int DayMonth { get; set; }
        public TheOrder TheOrder { get; set; }
        public TheWeekly TheWeekly { get; set; }
        public int TheMonth { get; set; }
        public DateTime FirstDate { get; set; }
        [DisplayName("Occurs every")]
        public bool ActionType { get; set; }
        public FreqSubdayType FreqSubdayType { get; set; }
        public int FreqSubdayInterval { get; set; }
        [DisplayName("End Time")]
        public TimeOnly EndTime { get; set; }
    }
}
