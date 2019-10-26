namespace MyDiabeticSystem.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Check
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="The field {0} is mandatory")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Carbohydrates { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Glucometry { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Hour")]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [DisplayFormat(DataFormatString = "{0:1900/01/01 HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Hour { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Bolus { get; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Hb1 { get; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateLocal => Date.ToLocalTime();

        [Display(Name = "Hour")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DisplayFormat(DataFormatString = "{0:1900/01/01 HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime HourLocal => Hour.ToLocalTime();

        public Patient Patient { get; set; }


    }
}
