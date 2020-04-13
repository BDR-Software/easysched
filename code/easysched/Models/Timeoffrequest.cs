using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Timeoffrequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Day { get; set; }
        public string Message { get; set; }
        public bool? Approved { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
