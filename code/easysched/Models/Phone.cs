using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Phone
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int? EmployeeId { get; set; }
        public int PhoneTypeId { get; set; }
        public int Number { get; set; }

        public virtual Company Company { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Phonetype PhoneType { get; set; }
    }
}
