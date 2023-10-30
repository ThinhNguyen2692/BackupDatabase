using ModelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Func
{
    public static class LibrarySchedule
    {
        public static string GetCronString(ScheduleBackup ScheduleBackup)
        {
            string CronString = "";
            var FirstDate = ScheduleBackup.FirstDate;
            CronString = FirstDate.ToString("ss mm HH");
            if (ScheduleBackup.Occurs == Occurs.Day)
            {
                string templ = ScheduleBackup.RecursEveryDay > 1 ? " */" + ScheduleBackup.RecursEveryDay : " *";
                CronString += templ + " * ?";
                return CronString;
            }
            if (ScheduleBackup.Occurs == Occurs.Weekly)
            {
                var Weekly = string.Empty;
                foreach (var item in ScheduleBackup.Weeklies)
                {
                    switch (item)
                    {
                        case ModelProject.Models.Weekly.Monday:
                            Weekly += ",MON";
                            break;
                        case ModelProject.Models.Weekly.Tuesday:
                            Weekly += ",TUE";
                            break;
                        case ModelProject.Models.Weekly.Wednesday:
                            Weekly += ",WED";
                            break;
                        case ModelProject.Models.Weekly.Thursday:
                            Weekly += ",THU";
                            break;
                        case ModelProject.Models.Weekly.Friday:
                            Weekly += ",FRI";
                            break;
                        case ModelProject.Models.Weekly.Saturday:
                            Weekly += ",SAT";
                            break;
                        case ModelProject.Models.Weekly.Sunday:
                            Weekly += ",SUN";
                            break;
                    }
                }
                Weekly = Weekly.Substring(1);

                CronString += ScheduleBackup.RecursEveryWeekly == 1 ? " ? *" : " ? */" + ScheduleBackup.RecursEveryWeekly;
                // 00 34 11 ? */ 2  MON,TUE,WED,THU,FRI,SAT,WED
                CronString += " " + Weekly;
                return CronString;
            }
            if (ScheduleBackup.Occurs == Occurs.Monthly)
            {
                CronString += " " + ScheduleBackup.DayEvery;
                CronString += ScheduleBackup.DayMonth == 1 ? " *" : " */" + ScheduleBackup.DayMonth;
                CronString += " ? *";
                return CronString;
            }
            return string.Empty;
        }
        public static string GetCronString(int month, int day)
        {
            string CronStringDetele = "0 0 0 " + day;
            CronStringDetele += month == 1 ? " *" : " */" + month;
            CronStringDetele += " ?";
            return CronStringDetele;
        }
    }
}
