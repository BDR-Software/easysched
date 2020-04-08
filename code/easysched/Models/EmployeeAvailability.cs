using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class EmployeeAvailability
    {
        public int Id { get; set; }
        public int WeekdayId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Weekday Weekday { get; set; }
    }
}
