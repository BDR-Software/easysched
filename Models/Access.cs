using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Access
    {
        public Access()
        {
            Employee = new HashSet<Employee>();
        }

        public uint Id { get; set; }
        public string Rights { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
