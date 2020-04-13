using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            Shift = new HashSet<Shift>();
        }

        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public virtual Company Company { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Shift> Shift { get; set; }
    }
}
