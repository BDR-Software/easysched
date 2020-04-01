using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Companylicence
    {
        public Companylicence()
        {
            Company = new HashSet<Company>();
        }

        public uint Id { get; set; }
        public double? Cost { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public uint? LicenceId { get; set; }

        public virtual Licence Licence { get; set; }
        public virtual ICollection<Company> Company { get; set; }
    }
}
