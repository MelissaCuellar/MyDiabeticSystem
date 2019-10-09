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

        public Patient Patient { get; set; }

    }
}
