using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace easysched.Models
{ 
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
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPass;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int EmployeeId;
    }

    [ModelMetadataType(typeof(LoginMetadata))]
    public partial class Login : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Pass != ConfirmPass)
            {
                yield return new ValidationResult("Passwords must match", new[] { nameof(ConfirmPass) });
            }
        }
    }
}
