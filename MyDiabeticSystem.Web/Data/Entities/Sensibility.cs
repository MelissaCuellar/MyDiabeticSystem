using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiabeticSystem.Web.Data.Entities
{
    public class Sensibility
    {
        public int Id { get; set; }

        [Display(Name = "Start Time")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public double Value { get; set; }

        [Display(Name = "Start Time")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTimeLocal => StartTime.ToLocalTime();

        [Display(Name = "End Time")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTimeLocal => EndTime.ToLocalTime();

        public Patient Patient { get; set; }
    }
}
