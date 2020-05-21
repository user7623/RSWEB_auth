using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspProekt.Models
{
    public class Enrollment
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string StudentID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [StringLength(10, MinimumLength = 3)]
        public string Smester { get; set; }
        public int Year { get; set; }
        [Range(1, 10)]
        public decimal Grade { get; set; }
        [StringLength(255, MinimumLength = 3)]
        public string SeminalUrl { get; set; }
        [StringLength(255, MinimumLength = 3)]
        public string ProjectUrl { get; set; }

        public int ExamPoints { get; set; }

        public int SeminalPoints { get; set; }

        public int AditionalPoints { get; set; }

        public int ProjectPoints { get; set; }

        public DateTime FinishDate { get; set; }

        //public ICollection<Course> mCourse { get; set; }
    }
}
