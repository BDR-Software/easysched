using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Licence
    {
        public Licence()
        {
            Companylicence = new HashSet<Companylicence>();
        }

        public uint Id { get; set; }
        public string Agreement { get; set; }

        public virtual ICollection<Companylicence> Companylicence { get; set; }
    }
}
