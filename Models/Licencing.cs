using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Licencing
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public double? Cost { get; set; }
        public TimeSpan? Start { get; set; }
        public TimeSpan? End { get; set; }

        public virtual Company Company { get; set; }
    }
}
