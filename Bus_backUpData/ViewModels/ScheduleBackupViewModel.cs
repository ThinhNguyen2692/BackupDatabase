﻿using Bus_backUpData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_backUpData.ViewModels
{
    public class ScheduleBackupViewModel
    {

        [DisplayName("Occurs")]
        public Occurs Occurs { get; set; }
        [DisplayName("Recurs every")]
        public int RecursEveryDay { get; set; }
        [DisplayName("Recurs every")]
        public int RecursEveryWeekly { get; set; }
        public List<Weekly> Weeklies { get; set; }
        public bool MonthlyDay { get; set; }
        public int DayEvery { get; set; }
        public int DayMonth { get; set; }
        public TheOrder TheOrder { get; set; }
        public TheWeekly TheWeekly { get; set; }
        public int TheMonth { get; set; }
        public DateTime FirstDate { get; set; }
    }
}