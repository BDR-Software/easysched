using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace easysched.Models
{
    public class TimeOffRequestMetadata
    {
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Day { get; set; }
    }

    [ModelMetadataType(typeof(TimeOffRequestMetadata))]
    public partial class Timeoffrequest { }
}
