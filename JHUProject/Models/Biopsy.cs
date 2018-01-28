using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JHUProject.Models
{
    public class Biopsy
    {
        [Key]
        public int BiopsyID { get; set; }
        [Required]
        [Display(Name = "Patient")]
        public int PatientID { get; set; }
        [Required]
        [Display(Name = "Clinician")]
        public int ClinicianID { get; set; }
        [Required]
        [Display(Name = "Medical Record Number")]
        public string RecordNumber { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Service Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ServiceDate { get; set; }
        [Required]
        [Display(Name = "Biopsy Site")]
        public string BiopsySite { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        
        public virtual Clinician Clinician { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
