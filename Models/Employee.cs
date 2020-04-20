using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeAvailability = new HashSet<EmployeeAvailability>();
            EmployeeSchedule = new HashSet<EmployeeSchedule>();
            Phone = new HashSet<Phone>();
        }

        public int Id { get; set; }
        public int PrivelegesId { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public string Email { get; set; }
        public double? Wages { get; set; }

        public virtual Company Company { get; set; }
        public virtual Priveleges Priveleges { get; set; }
        public virtual ICollection<EmployeeAvailability> EmployeeAvailability { get; set; }
        public virtual ICollection<EmployeeSchedule> EmployeeSchedule { get; set; }
        public virtual ICollection<Phone> Phone { get; set; }
    }
}
