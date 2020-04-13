using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace easysched.Models
{
    public class ScheduleMetadata
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Start Date")]
        public DateTime? Start { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "End Date")]
        public DateTime? End { get; set; }
    }

    [ModelMetadataType(typeof(ScheduleMetadata))]
    public partial class Schedule
    {
        public string StartEnd
        {
            get
            {
                return Start.Value.ToString("MMM dd (ddd) yyyy") + " - " + End.Value.ToString("MMM dd (ddd) yyyy");
            }
        }
    }
}
