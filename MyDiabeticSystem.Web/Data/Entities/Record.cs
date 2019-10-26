using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiabeticSystem.Web.Data.Entities
{
    public class Record
    {
        public int Id { get; set; }

        [Display(Name = "Modification date")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime ModificationDate { get; set; }

        [Required (ErrorMessage = "The field {0} is mandatory.")]
        public string Description { get; set; }

        public string Justification { get; set; }

        public string User { get; set; }

        [Display(Name = "Modification date")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime ModificationDateLocal => ModificationDate.ToLocalTime();

        public Patient Patient { get; set; }

    }
}
