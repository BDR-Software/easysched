using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            EmployeeSchedule = new HashSet<EmployeeSchedule>();
        }

        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public DateTime Week { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<EmployeeSchedule> EmployeeSchedule { get; set; }
    }
}
