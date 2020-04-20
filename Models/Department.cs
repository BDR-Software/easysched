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
        public string Name { get; set; }

        public virtual ICollection<Schedule> Schedule { get; set; }
    }
}
