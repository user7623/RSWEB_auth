using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspProekt.Models
{
    public class Teacher
    {
        [Required]
        public int ID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Degree")]
        public string Degree { get; set; }
        [StringLength(25, MinimumLength = 3)]
        [Display(Name = "Academic Rank")]
        public string AcademicRank { get; set; }
        [StringLength(10, MinimumLength = 3)]
        [Display(Name = "Office number")]
        public string OfficeNumber { get; set; }

        public string pic { get; set; }
        public DateTime HireDate { get; set; }
    }
}