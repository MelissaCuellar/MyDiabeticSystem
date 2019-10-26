using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyDiabeticSystem.Web.Data.Entities
{

    public class Doctor
    {
        public int Id { get; set; }

        public User User { get; set; }
              
        public ICollection<Patient> Patients { get; set; }
    }

}

