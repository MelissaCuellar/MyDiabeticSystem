using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiabeticSystem.Web.Data.Entities
{
    public class Ratio
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "The field {0} is mandatory.")]
        public TimeSpan StartTime {get; set;}

        [Required (ErrorMessage = "The field {0} is mandatory.")]
        public TimeSpan EndTime { get; set; }

        [Required (ErrorMessage = "The field {0} is mandatory.")]
        public double Value { get; set; }

        public Patient Patient { get; set; }
    }
}
