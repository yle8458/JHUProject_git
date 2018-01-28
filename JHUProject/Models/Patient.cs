using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;

namespace JHUProject.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Register Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegisterDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate{ get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public virtual ICollection<Biopsy> Biopsies { get; set; }
    }

}
