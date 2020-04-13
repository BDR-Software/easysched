using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeAvailability = new HashSet<EmployeeAvailability>();
            Login = new HashSet<Login>();
            Phone = new HashSet<Phone>();
            Shift = new HashSet<Shift>();
            Timeoffrequest = new HashSet<Timeoffrequest>();
        }

        public int Id { get; set; }
        public int PrivelegesId { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? EmployeeNumber { get; set; }
        public string Email { get; set; }
        public double? Wages { get; set; }

        public virtual Company Company { get; set; }
        public virtual Priveleges Priveleges { get; set; }
        public virtual ICollection<EmployeeAvailability> EmployeeAvailability { get; set; }
        public virtual ICollection<Login> Login { get; set; }
        public virtual ICollection<Phone> Phone { get; set; }
        public virtual ICollection<Shift> Shift { get; set; }
        public virtual ICollection<Timeoffrequest> Timeoffrequest { get; set; }
    }
}
