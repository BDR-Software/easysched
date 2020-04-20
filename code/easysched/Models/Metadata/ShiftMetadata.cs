using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace easysched.Models
{
    public class ShiftMetadata
    {
        [Required]
        public int EmployeeId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Day { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Display(Name = "Start Time")]
        public DateTime? Start { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Display(Name = "End Time")]
        public DateTime? End { get; set; }
    }

    [ModelMetadataType(typeof(ShiftMetadata))]
    public partial class Shift
    {
        public string StartEnd
        {
            get
            {
                return Day.Value.ToString("MMM dd (ddd) yyyy") + ": " + Start.Value.ToString("HH:mm") + " - " + Start.Value.ToString("HH:mm");
            }
        }
    }
}
