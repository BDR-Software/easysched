using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Employee
    {
        public uint Id { get; set; }
        public uint AccessId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Employeeid { get; set; }
        public string Email { get; set; }
        public uint? PhoneNumberId { get; set; }

        public virtual Access Access { get; set; }
        public virtual Phonenumber PhoneNumber { get; set; }
    }
}
