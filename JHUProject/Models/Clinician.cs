using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JHUProject.Models
{
    public class Clinician
    {
        [Key]
        public int ClinicianID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Biopsy> Biopsies { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

    }
}
