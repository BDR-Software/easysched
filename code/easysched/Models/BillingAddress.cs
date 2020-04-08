using System;
using System.Collections.Generic;

namespace easysched.Models
{
    public partial class BillingAddress
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Street { get; set; }
        public int? PostalCode { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

        public virtual Company Company { get; set; }
    }
}
