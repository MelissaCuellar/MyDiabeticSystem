using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiabeticSystem.Web.Data.Entities
{
    public class User:IdentityUser
    {
        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }       

        [Display(Name = "Date of birth")]      
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime  DateBirth { get; set; }

        [Display(Name = "Father's mail")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [EmailAddress]
        public string FathersEmail { get; set; }

        [Display(Name = "Can edit?")]
        public bool ? CanEdit { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        [Display(Name = "Date of birth")]       
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateBirthLocal => DateBirth.ToLocalTime();
    }
}
