using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiabeticSystem.Web.Models
{
    public class AddCheckViewModel
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public double Carbohydrates { get; set; } 

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public double Glucometry { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public double Hb1 { get; set; }

        [Display(Name = "Hour")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Hour { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Patient")]
        public int PatientId { get; set; }

        [Display(Name = "Hour")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTimeLocal => Hour.ToLocalTime();
    }
}
