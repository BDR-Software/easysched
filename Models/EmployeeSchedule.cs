using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class EmployeeSchedule
    {
        public int ScheduleId { get; set; }
        public int EmployeeId { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}
