using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Phonenumber
    {
        public Phonenumber()
        {
            Company = new HashSet<Company>();
            Employee = new HashSet<Employee>();
        }

        public uint Id { get; set; }
        public uint? PhonetypeId { get; set; }
        public string Number { get; set; }

        public virtual Phonetype Phonetype { get; set; }
        public virtual ICollection<Company> Company { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
