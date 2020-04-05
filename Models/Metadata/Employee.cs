using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace easysched.Models
{
    [ModelMetadataType(typeof(EmployeeMetadata))]
    public partial class Employee { }
    public class EmployeeMetadata
    {
        [HiddenInput(DisplayValue = true)]
        public int Id { get; set; }
        public int PrivelegesId { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Employee Number")]
        public string EmployeeNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        public double? Wages { get; set; }
    }
}
