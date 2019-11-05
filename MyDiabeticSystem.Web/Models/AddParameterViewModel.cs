using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiabeticSystem.Web.Models
{
    public class AddParameterViewModel
    {
        [Display(Name = "Description")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Description { get; set; }

        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? StartTime { get; set; }

        [Display(Name = "End Time")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public double Value { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Patient")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a doctor.")]
        public int PatientId { get; set; }


    }
}
