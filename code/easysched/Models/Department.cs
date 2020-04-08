using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Department
    {
        public Department()
        {
            Schedule = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string Name { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
    }
}
