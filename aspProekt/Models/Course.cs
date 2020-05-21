using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspProekt.Models
{
    public class Course
    {
        [Required]
        public int ID { get; set; }
        [StringLength(100, MinimumLength = 3)]
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        public int Credits { get; set; }

        public int Semester { get; set; }
        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Programe { get; set; }
        [StringLength(25, MinimumLength = 3)]
        public string EducationLevel { get; set; }

        public int FirstTeacherID { get; set; }

        public int SecondTeacherID { get; set; }

        public ICollection<Enrollment> Students { get; set; }

    }
}