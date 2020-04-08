using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace easysched.Models
{
    [ModelMetadataType(typeof(ShiftMetadata))]
    public partial class Shift { }
    public class ShiftMetadata
    {
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Display(Name = "Start Time")]
        public DateTime? StartTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Display(Name = "End Time")]
        public DateTime? EndTime { get; set; }
    }
}
