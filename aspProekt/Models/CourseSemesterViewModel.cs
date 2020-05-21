using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspProekt.Models
{
    public class CourseSemesterViewModel
    {
        public List<Course> Courses { get; set; }

        public SelectList Semesters { get; set; }
        public string CourseSemester { get; set; }
        public string SearchSemester { get; set; }
        public string SearchString { get; set; }
        public string SearchPrograma { get; set; }

    }
}
