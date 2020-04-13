using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Company
    {
        public Company()
        {
            BillingAddress = new HashSet<BillingAddress>();
            Department = new HashSet<Department>();
            Employee = new HashSet<Employee>();
            Licencing = new HashSet<Licencing>();
            Phone = new HashSet<Phone>();
            Schedule = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyNumber { get; set; }

        public virtual ICollection<BillingAddress> BillingAddress { get; set; }
        public virtual ICollection<Department> Department { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Licencing> Licencing { get; set; }
        public virtual ICollection<Phone> Phone { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
    }
}
