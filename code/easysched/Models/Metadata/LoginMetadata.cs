using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace easysched.Models
{
    [ModelMetadataType(typeof(LoginMetadata))]

    public partial class Login { }
    public class LoginMetadata
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email;

        [Required]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Pass;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int EmployeeId;
    }
}
