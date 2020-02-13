using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class Company
    {
        public uint Id { get; set; }
        public uint? CompanyLicenseId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public uint? PhoneNumberId { get; set; }

        public virtual Companylicence CompanyLicense { get; set; }
        public virtual Phonenumber PhoneNumber { get; set; }
    }
}
