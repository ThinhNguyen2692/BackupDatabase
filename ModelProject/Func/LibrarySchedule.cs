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
                if(ScheduleBackup.ActionType == true)
                {
                    var sec = FirstDate.ToString("ss");
                    var Min = FirstDate.ToString("mm");
                    var hou = FirstDate.ToString("HH");
                    var everstring = "/" + ScheduleBackup.FreqSubdayInterval;
                    var EndTimeMin = ScheduleBackup.EndTime.ToString("mm");
                    var EndTimeHou = ScheduleBackup.EndTime.ToString("HH");
                    switch (ScheduleBackup.FreqSubdayType)
                    {
                        case FreqSubdayType.Seconds: 
                            sec = sec + everstring;
                            Min = $"{Min}-{EndTimeMin}";
                            hou = $"{hou}-{EndTimeHou}";
                            break;
                        case FreqSubdayType.Minutes:
                            Min = $"{Min}-{EndTimeMin}{everstring}";
                            hou = $"{hou}-{EndTimeHou}";
                            break;
                        case FreqSubdayType.Hours:
                            Min = $"{Min}-{EndTimeMin}";
                            hou = $"{hou }-{EndTimeHou}{everstring}"; 
                            break;
                    }
                    CronString = $"{sec} {Min} {hou}";
                }
                
                CronString += templ + " * ?";
                return CronString;
            }
            if (ScheduleBackup.Occurs == Occurs.Weekly)
            {
                var Weekly = string.Empty;
                foreach (var item in ScheduleBackup.ScheduleBackupWeeklies)
                {
                    switch (item.Weekly.Value)
                    {
                        case 2:
                            Weekly += ",MON";
                            break;
                        case 4:
                            Weekly += ",TUE";
                            break;
                        case 8:
                            Weekly += ",WED";
                            break;
                        case 16:
                            Weekly += ",THU";
                            break;
                        case 32:
                            Weekly += ",FRI";
                            break;
                        case 64:
                            Weekly += ",SAT";
                            break;
                        case 1:
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
