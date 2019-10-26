using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiabeticSystem.Web.Data.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        
        public User User { get; set; }

        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateBirth { get; set; }

        [Display(Name = "Can edit?")]
        public bool CanEdit { get; set; } 

        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateBirthLocal => DateBirth.ToLocalTime();
      

    }
}
