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
     
        public Doctor Doctor { get; set; }

        public ICollection<Ratio> Ratios { get; set; }

        public ICollection<Sensibility> Sensibilities { get; set; }

    }
}
