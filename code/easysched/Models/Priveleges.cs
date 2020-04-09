using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Priveleges
    {
        public Priveleges()
        {
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public int? Type { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
