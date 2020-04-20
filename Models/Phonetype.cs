using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Phonetype
    {
        public Phonetype()
        {
            Phone = new HashSet<Phone>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Phone> Phone { get; set; }
    }
}
