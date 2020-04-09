using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Login
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
