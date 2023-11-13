using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject.Models.Enum
{
    public class ScheduleBackupWeekly :Entity
    {
        public Guid ScheduleBackupId { get; set; }
        public Guid WeeklyId { get; set; }
        [ForeignKey("ScheduleBackupId")]
        public ScheduleBackup ScheduleBackup { get; set; }
        [ForeignKey("WeeklyId")]
        public Weekly Weekly { get; set; }
    }
}
