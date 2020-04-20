using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Weekday
    {
        public Weekday()
        {
            EmployeeAvailability = new HashSet<EmployeeAvailability>();
        }

        public int Id { get; set; }
        public string Day { get; set; }

        public virtual ICollection<EmployeeAvailability> EmployeeAvailability { get; set; }
    }
}
