using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Employeelog
    {
        public uint Id { get; set; }
        public uint Employeeid { get; set; }
        public string Ipaddress { get; set; }
        public DateTime Time { get; set; }
        public string Log { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
