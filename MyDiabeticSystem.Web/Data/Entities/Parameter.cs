using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiabeticSystem.Web.Data.Entities
{
    public class Parameter
    {
        
        public int Id { get; set; }

        [Required (ErrorMessage = "The field {0} is mandatory.")]
        public string Description { get; set; }

        [Required (ErrorMessage = "The field {0} is mandatory.")]
        public double Value { get; set; }

        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0:1900/01/01 HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? StartTime { get; set; }

        [Display(Name = "End Time")]
        [DisplayFormat(DataFormatString = "{0:1900/01/01 HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }

        public Patient Patient { get; set; }

    }
}
