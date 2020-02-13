using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Phonetype
    {
        public Phonetype()
        {
            Phonenumber = new HashSet<Phonenumber>();
        }

        public uint Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Phonenumber> Phonenumber { get; set; }
    }
}
