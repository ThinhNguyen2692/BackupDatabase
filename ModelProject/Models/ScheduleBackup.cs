using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Models
{

    public enum Occurs
    {
        [Description("Day")]
        Day = 4,
        [Description("Weekly")]
        Weekly = 8,
        [Description("Monthly")]
        Monthly = 16,
    }

    public enum Weekly
    {
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64,
        Sunday = 1,
    }

    public enum TheWeekly
    {
        [Description("Monday")]
        Monday = 2,
        [Description("Tuesday")]
        Tuesday = 3,
        [Description("Webnesday")]
        Webnesday = 4,
        [Description("Thursday")]
        Thursday = 5,
        [Description("Friday")]
        Friday = 6,
        [Description("Saturday")]
        Saturday = 7 ,
        [Description("Sunday")]
        Sunday = 1,
        [Description("Day")]
        Day = 8 ,
        [Description("Weekday")]
        Weekday =9,
        [Description("WeekendDay")]
        WeekendDay= 10,
    }

    public enum TheOrder
    {
        first = 1,
        second = 2,
        third = 4,
        fourth = 8,
        last = 16
    }

    public enum FreqSubdayType
    {
        Seconds = 2,
        Minutes = 4,
        Hours = 8
    }

    public class ScheduleBackup
    {
        public Occurs Occurs { get; set; }
        public int RecursEveryDay { get; set; }
        public int RecursEveryWeekly { get; set; }
        public List<Weekly> Weeklies { get; set; }
        public bool MonthlyDay { get; set; }
        public bool MonthlyThe { get; set; }
        public int DayEvery { get; set; }
        public int DayMonth { get; set; }
        public TheOrder TheOrder { get; set; }
        public TheWeekly TheWeekly { get; set; }
        public int TheMonth { get; set; }
        public DateTime FirstDate { get; set; }
        public bool ActionType { get; set; }
        public FreqSubdayType FreqSubdayType { get; set; }
        public int FreqSubdayInterval { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
